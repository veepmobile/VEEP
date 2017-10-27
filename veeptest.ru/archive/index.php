<?php

function file_force_download($file) {
  if (file_exists($file)) {
    // сбрасываем буфер вывода PHP, чтобы избежать переполнения памяти выделенной под скрипт
    // если этого не сделать файл будет читаться в память полностью!
    if (ob_get_level()) {
      ob_end_clean();
    }
    // заставляем браузер показать окно сохранения файла
    header('Content-Description: File Transfer');
    header('Content-Type: application/octet-stream');
    header('Content-Disposition: attachment; filename=' . basename($file));
    header('Content-Transfer-Encoding: binary');
    header('Expires: 0');
    header('Cache-Control: must-revalidate');
    header('Pragma: public');
    header('Content-Length: ' . filesize($file));
    // читаем файл и отправляем его пользователю
    readfile($file);
    exit;
  }
}



define('MODX_API_MODE', true);
require '../index.php';

if (!$modx->user->isAuthenticated('mgr')) {
    die('Access denied');
}

if (!isset($_GET['type']) || !isset($_GET['filename'])) {
    die('Not enough parameters.');
}

if (!preg_match('/\.(sql)|(zip)$/', $_GET['filename'])) {
    die('Unknown file extension.');
}

$dir = '';
switch ($_GET['type']) {
    case 'dump':
        $dir = 'dumps';
        break;
    case 'archive':
        $dir = 'archives';
        break;
}

$file_name = __DIR__ . "/$dir/" . preg_replace('/[^\w-\.]/', '', $_GET['filename']);

file_force_download($file_name);
