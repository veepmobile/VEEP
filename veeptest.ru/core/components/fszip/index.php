<?php

namespace fszip;

require_once 'lib/ifsnop-mysqldump.php';

require_once 'Tpl.class.php';
require_once 'Dumper.class.php';
require_once 'Archiver.class.php';

$obTpl = new Tpl('tpl/');

$action = isset($_POST['action']) ? $_POST['action'] : '';

$archive_dir = 'archive/';

switch ($action) {
    case 'make-dump':
        $output = array();
        try {
            $file_name = 'dump_' . date('d-m-Y') . '.sql';
            $file_location = $archive_dir . 'dumps/' . $file_name;
            Dumper::toFile(MODX_BASE_PATH . $file_location);
            $output['success'] = true;
            $output['href'] = '/' . $archive_dir . '?type=dump&filename=' . $file_name;
            $output['success_message'] = 'Дамп создан';
            $output['link_text'] = 'Скачать дамп';
        } catch (Exception $e) {
            $output['success'] = false;
            $output['error_message'] = $e->getMessage();
        }
        return $obTpl->render('process-result', $output);
        break;

    case 'make-archive':
        $output = array();
        try {
            $file_name = 'backup_' . date('d-m-Y') . '.zip';
            $file_location = $archive_dir . 'archives/' . $file_name;
            Archiver::toFile(MODX_BASE_PATH . $file_location);
            $output['success'] = true;
            $output['href'] = '/' . $archive_dir . '?type=archive&filename=' . $file_name;
            $output['success_message'] = 'Архив создан';
            $output['link_text'] = 'Скачать архив';
        } catch (Exception $e) {
            $output['success'] = false;
            $output['error_message'] = $e->getMessage();
        }
        return $obTpl->render('process-result', $output);
        break;

    // сделать дамп и архив
    case 'make-all':
        $output = array();
        try {
            $dump_file_name = 'dump_' . date('d-m-Y') . '.sql';
            $file_location = $archive_dir . 'dumps/' . $dump_file_name;
            Dumper::toFile(MODX_BASE_PATH . $file_location);

            $archive_file_name = 'backup_' . date('d-m-Y') . '.zip';
            $file_location = $archive_dir . 'archives/' . $archive_file_name;
            Archiver::toFile(MODX_BASE_PATH . $file_location);

            $output['success'] = true;
            $output['href'] = '/' . $archive_dir . '?type=archive&filename=' . $archive_file_name;
            $output['success_message'] = 'Архив (с дампом) создан';
            $output['link_text'] = 'Скачать архив';
        } catch (Exception $e) {
            $output['success'] = false;
            $output['error_message'] = $e->getMessage();
        }
        return $obTpl->render('process-result', $output);
        break;

    default:
        return $obTpl->render('index');
}
