<?php  return '/*
getSiteSettings ver: 1.9
Igor Siluyanov, 16.11.2016

Скрипт получает все значения доп. полей ресурса, содержащего настройки, и создаёт одноимённые плейсхолдеры с префиксом "set.", пример:
[[+set.site-slogan]]
Контакты хранятся в migx-е, поэтому для них предусмотрена обработка и вывод сразу с иконками и ссылками.
Выводятся 2 списка контактов: [[+set.contacts.header]] и [[+set.contacts.all]] (если у контакта стоит галка dont_show, то он не появится в этих списках)
А также каждый контакт выводится в таком виде: [[+set.contacts.email1]] и в таком: [[+set.unique_name]] (если у контакта установлено unique_name)
Адрес почты в списках "всех контактов" и "для хедера" выводятся через сниппет botaway

Параметры:
    contactTpl - шаблон для каждого контакта (нужно указать имя чанка, либо html код с префиксом "@INLINE "),
        значение по умолчанию: \'<p class="[[+class]]"><span>[[+icon]] <strong>[[+text]]</strong> [[+value]]</span></p>\'
    settings_res_id - id ресурса, содержащего настройки
*/

class GetSiteSettings
{
    private $default_contact_tpl = \'<p class="[[+class]]"><span>[[+icon]] <strong>[[+text]]</strong> [[+value]]</span></p>\';

    public function __construct($modx, $scriptProperties)
    {
        $this->modx = $modx;
        $this->settings_res_id = $scriptProperties[\'settings_res_id\'];

        // устанавливаем шаблон контакта (из чанка, из инлайна или дефолтный)
        if (isset($scriptProperties[\'contactTpl\'])) {
            if (strpos($scriptProperties[\'contactTpl\'], \'@INLINE\') === 0) {
                $this->contact_tpl = substr($scriptProperties[\'contactTpl\'], 8);
            } else {
                $this->contact_tpl = $this->modx->getChunk($scriptProperties[\'contactTpl\']);
            }
        } else {
            $this->contact_tpl = $this->default_contact_tpl;
        }

        // получаем все "настройки сайта"
        $stmt = $this->modx->query("
            SELECT tv.name, tv_c.value
            FROM modx_site_tmplvars as tv

            LEFT JOIN modx_site_tmplvar_contentvalues as tv_c
            ON tv.id = tv_c.tmplvarid and tv_c.contentid = " . $this->settings_res_id . "
        ");

        $placeholders = array();
        while ($row = $stmt->fetch()) {
            if ($row[\'name\'] == \'contacts\') { // для тв-поля contacts - особая обработка:
                $contacts = $this->processContacts($row[\'value\']);
                $placeholders = array_merge($placeholders, $contacts);
            } else { // важно: если значение пустое, то присваиваем ему пустую строку
                $pl_name = \'set.\' . $row[\'name\'];
                $placeholders[$pl_name] = $row[\'value\'] ? $row[\'value\'] : \'\';
            }
        }
        $modx->setPlaceholders($placeholders); // установка плейсхолдеров - весь результат работы скрипта
    }

    private function fixLink($link)
    {
        if (strpos($link, \'http\') !== 0) {
            $link = \'http://\' . $link;
        }
        return $link;
    }

    private function clearPhone($input)
    {
        return preg_replace(array(\'/[^\\d]/u\', \'/^[7|8]/u\'), array(\'\', \'+7\'), $input);
    }

    private function processContacts($json_str)
    {
        $contacts_arr = json_decode($json_str);
        $result = array();
        $item_counter = array(); // счётчик для каждого из типов контактов
        foreach($contacts_arr as $contact) {
            $item_html = \'\';
            // ужасная проверка по строкам на русском языке, но иначе нельзя:
            switch($contact->contact_type) {
                case \'Телефон\':
                    $item_icon = \'<i class="fa fa-fw fa-phone"></i>\';
                    $cleared_phone = $this->clearPhone($contact->contact_value);
                    $item_value_wrapped = \'<a href="tel:\' . $cleared_phone . \'">\' . $contact->contact_value . \'</a>\';
                    $item_type = \'phone\';
                    break;
                case \'Факс\':
                    $item_icon = \'<i class="fa fa-fw fa-fax"></i>\';
                    $cleared_phone = $this->clearPhone($contact->contact_value);
                    $item_value_wrapped = \'<a href="tel:\' . $cleared_phone . \'">\' . $contact->contact_value . \'</a>\';
                    $item_type = \'fax\';
                    break;
                case \'E-mail\':
                    $item_icon = \'<i class="fa fa-fw fa-envelope-o"></i>\';
                    $item_value_wrapped = $this->modx->runSnippet(\'botaway\', array(\'email\' => $contact->contact_value));
                    //$item_value_wrapped = \'<a href="mailto:\' . $contact->contact_value . \'">\' . $contact->contact_value . \'</a>\';
                    $item_type = \'email\';
                    break;
                case \'Адрес\':
                    $item_icon = \'<i class="fa fa-fw fa-map-marker"></i>\';
                    $item_value_wrapped = $contact->contact_value;
                    $item_type = \'address\';
                    break;
                case \'Skype\':
                    $item_icon = \'<i class="fa fa-fw fa-skype"></i>\';
                    $item_value_wrapped = $contact->contact_value;
                    $item_type = \'skype\';
                    break;
                case \'ВКонтакте\':
                    $item_icon = \'<i class="fa fa-fw fa-vk"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">ВКонтакте</a>\';
                    $item_type = \'vk\';
                    break;
                case \'Facebook\':
                    $item_icon = \'<i class="fa fa-fw fa-facebook-official"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">Facebook</a>\';
                    $item_type = \'facebook\';
                    break;
                case \'Instagram\':
                    $item_icon = \'<i class="fa fa-fw fa-instagram"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">Instagram</a>\';
                    $item_type = \'instagram\';
                    break;
                case \'Веб-сайт\':
                    $item_icon = \'<i class="fa fa-fw fa-external-link"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">\' . $link . \'</a>\';
                    $item_type = \'website\';
                    break;
                case \'Время\':
                    $item_icon = \'<i class="fa fa-fw fa-clock-o" aria-hidden="true"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">\' . $link . \'</a>\';
                    $item_type = \'time\';
                    break;
                case \'Текст\':
                    $item_icon = \'\';
                    $item_value_wrapped = $contact->contact_value;
                    $item_type = \'text\';
                    break;
            }

            $item_html = str_replace(
                array(\'[[+icon]]\', \'[[+value]]\', \'[[+text]]\', \'[[+class]]\'),
                array($item_icon, $item_value_wrapped, $contact->contact_text, $contact->css_class),
                $this->contact_tpl
            );

            // значения contacts.all и contacts.header
            if ($contact->contact_value && !$contact->dont_show) {
                $result[\'set.contacts.all\'] .= $item_html;
                if ($contact->display_at_header) {
                    $result[\'set.contacts.header\'] .= $item_html;
                }
            }

            // значения плейсхолдеров со счётчиками
            $item_counter[$item_type]++;
            $item_placeholder = \'set.contacts.\' . $item_type . $item_counter[$item_type];
            $result[$item_placeholder] = $contact->contact_value;
            // доптекст поля
            $item_text_placeholder = \'set.contacts.\' . $item_type . $item_counter[$item_type] . \'.text\';
            $result[$item_text_placeholder] = $contact->contact_text;

            // выкидываем лишние символы из уникального имени
            $contact->unique_name = preg_replace(\'/[^\\w-]/\', \'\', $contact->unique_name);
            // добавляем уникальное имя контакта (если оно заполнено)
            if ($contact->unique_name) {
                $result[\'set.\' . $contact->unique_name] = $contact->contact_value;
                $result[\'set.\' . $contact->unique_name . \'.text\'] = $contact->contact_text;
            }

            //
        }
        return $result;
    }
}

new GetSiteSettings($modx, $scriptProperties);
return;
';