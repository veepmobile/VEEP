<?php

namespace fszip;

class Dumper
{
    public static function toFile($file_location)
    {
        global $database_server;
        global $dbase;
        global $database_user;
        global $database_password;

        $dump = new \Mysqldump(
            "mysql:host=$database_server;dbname=$dbase",
            $database_user,
            $database_password,
            array(
                'add-drop-table' => true
            )
        );
        $dump->start($file_location);
    }
}
