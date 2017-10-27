<?php

namespace fszip;

class Archiver
{
    public static function toFile($file_location)
    {
        set_time_limit(0);

        // внимание, слеш в конце обязателен!
        $dirs_to_ignore = [
            '/core/export/',
            '/core/import/',
            '/core/cache/',
            '/archive/archives/',
            '/assets/components/gallery/cache/'
        ];

        if (file_exists($file_location)) {
            unlink($file_location);
        }
        if (!self::zip(MODX_BASE_PATH, $file_location, $dirs_to_ignore) || !file_exists($file_location)) {
            throw new Exception('Архив не создан');
        }
    }

    private static function zip($source, $destination, $dirs_to_ignore = [])
    {
        // добавляем слеши в конец имён папок если их там нет - они будут нужны
        foreach ($dirs_to_ignore as $key => $dir_name) {
            if (!preg_match('/\/$/', $dir_name)) {
                $dirs_to_ignore[$key] = $dir_name . '/';
            }
        }

        $source = realpath($source); // также здесь у $source пропадает слеш в конце

        if (!(extension_loaded('zip') && file_exists($source))) {
            return false;
        }

        $zip = new \ZipArchive();

        if ($zip->open($destination, \ZIPARCHIVE::CREATE) !== true) {
            return false;
        }

        if (is_dir($source)) {
            $files = new \RecursiveIteratorIterator(
                new \RecursiveDirectoryIterator($source),
                \RecursiveIteratorIterator::SELF_FIRST
            );
            foreach ($files as $file) {
                // проверяем, не из игнорируемой ли папки этот файл или папка
                $file_from_ignored_dir = false;
                /*
                тут сама игнорируемая папка не попадает в игнорируемые, т. к. условие отрабатывет так:
                strpos('/home/hosts/test4/core/cache', '/home/hosts/test4' . '/core/cache/')
                */
                foreach ($dirs_to_ignore as $ign_dir) {
                    if (strpos($file, $source . $ign_dir) !== false) {
                        $file_from_ignored_dir = true;
                        break;
                    }
                }
                if ($file_from_ignored_dir) {
                    continue;
                }

                $file_local_path = str_replace($source . '/', '', $file);
                if (is_dir($file)) { // если это папка и она не начинается с точки
                    if (!preg_match('/^\./', basename($file))) {
                        $zip->addEmptyDir($file_local_path);
                    }
                } else if (is_file($file)) { // иначе, если это файл
                    $zip->addFromString($file_local_path, file_get_contents($file));
                }
            }
        } else if (is_file($source)) {
            $zip->addFromString(basename($source), file_get_contents($source));
        } else {
            return false;
        }
        return $zip->close();
    }
}
