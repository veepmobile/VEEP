<?php
$dir = '../install/';

if (isset($_POST['action']) && $_POST['action'] == 'del-dir') {
  delDir($dir);
}


function delDir($dir) {
  if (file_exists($dir) && is_dir($dir)) {
    foreach (glob($dir.'*') as $file) {
      if(is_dir($file)) {
        delDir($file."/");
      } else {
        unlink($file);
      }
    }
    rmdir($dir);
  }
}