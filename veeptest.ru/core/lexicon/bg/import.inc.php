<?php
/**
 * Import English lexicon entries
 *
 * @language en
 * @package modx
 * @subpackage lexicon
 */
$_lang['import_allowed_extensions'] = 'Задайте за импортиране разделен със запетаи списък на файлови разширения.<br /><small><em>оставете празно за да импортирате всички файлове вспоред типовете съдържание налично във вашия сайт. Неизвестните видове ще бъдат нанесени като обикновен текст.</em></small>';
$_lang['import_base_path'] = 'Въведете пътя на базовия файл, съдържащ файловете за импортиране.<br /><small><em>Оставете празно, за да използвате настройките на целевия контекст за път до файла.</em></small>';
$_lang['import_duplicate_alias_found'] = 'Ресурса [[+id]] вече използва псевдонима [[+alias]]. Моля въведете несъществуващ псевдоним.';
$_lang['import_element'] = 'Въведете основния HTML елемент за импортиране:';
$_lang['import_element_help'] = 'Provide JSON with associations "field":"value". If value starts with $ it is jQuery-like selector. Field can be a Resource field or TV name.';
$_lang['import_enter_root_element'] = 'Въведете основния елемент за импортиране:';
$_lang['import_files_found'] = '<strong>намерени %s документи за импортиране...</strong><p/>';
$_lang['import_parent_document'] = 'Родителски документ:';
$_lang['import_parent_document_message'] = 'Използвайте дървовидната структура на документа по-долу, за да изберете главната (родителска) локация, където да импортирате файловете.';
$_lang['import_resource_class'] = 'Изберете modResource клас за импортиране:<br /><small><em>Използвайте modStaticResource за да свържете статични файлове или modDocument за да копирате съдържанието в базата данни.</em></small>';
$_lang['import_site_failed'] = '<span style="color:#990000">Неуспешно!</span>';
$_lang['import_site_html'] = 'Импортирай сайт от HTML';
$_lang['import_site_importing_document'] = 'Импортиране на файл <strong>%s</strong> ';
$_lang['import_site_maxtime'] = 'Максимално време за импортиране:';
$_lang['import_site_maxtime_message'] = 'Тук може да зададете секундите за които Мениджъра на съдържанието може да импортира сайта (презаписвайки PHP настройките). Въведете 0 за неограничено време. Моля отбележете, че настройка 0 или наистина високо число може да доведе до странни процеси и смущения в сървъра и не се препоръчва.';
$_lang['import_site_message'] = '<p>Използвайки този инструмент, може да импортирате съдържанието от набора HTML файлове в базата данни. <em>Моля отбележете, че ще трябва да копирате вашите файлове и/или папки в ядрото/импорт папката.</em></p><p>Моля попълнете опцийте във формуляра по-долу, по желание изберете главен/родителски ресурс за импортираните файлове от дървовидната структура на документа и натиснете \'Импортирай HTML\' за да започне процеса на импортиране. Импортираните файлове ще бъдат запазени в избраната локация, използвайки, където е възможно името на файла като псевдоним на документа, а заглавието на страницата като заглавие на документа.</p>';
$_lang['import_site_resource'] = 'Импортиране на ресурси от статичен файл';
$_lang['import_site_resource_message'] = '<p>Използвайки този инструмент, може да импортирате ресурси от набора статични  файлове в базата данни.<em>Моля отбележете, че ще трябва да копирате вашите файлове и/или папки в ядрото/импорт папката.</em></p><p>Моля попълнете опцийте във формуляра по-долу, по желание изберете главен/родителски ресурс за импортираните файлове от дървовидната структура на документа и натиснете \'Импортирай ресурси\' за да започне процеса на импортиране. Импортираните файлове ще бъдат запазени в избраната локация, използвайки, където е възможно името на файла като псевдоним на документа и ако е HTML, заглавието на страницата като заглавие на документа.</p>';
$_lang['import_site_skip'] = '<span style="color:#990000">Пропуснато!</span>';
$_lang['import_site_start'] = 'Започни импортиране';
$_lang['import_site_success'] = '<span style="color:#009900">Успешно!</span>';
$_lang['import_site_time'] = 'Импортирането завършено. Импортирането отне %s секунди.';
$_lang['import_use_doc_tree'] = 'Използвайте дървовидната структура на документа по-долу, за да изберете главната (родителска) локация, където да импортирате файловете.';