<?php
/**
 *  MODX Configuration file
 */
$database_type = 'mysql';
$database_server = 'mysqlhost';
$database_user = 'nikolay31_fs';
$database_password = 'ECOkEUPa';
$database_connection_charset = 'utf8';
$dbase = 'nikolay31_fs';
$port = '';
$table_prefix = 'modx_';
$database_dsn = 'mysql:host=' . $database_server . ';dbname=' . $dbase . ';charset=utf8';
if ($port) {
    $database_dsn .= ';port=' . $port;
}
$config_options = array();
$driver_options = array();

$lastInstallTime = 1313395041;

$site_id = 'modx4e48d1615cf8b4.20731687';
$site_sessionname = 'SN4e48d11397491';
$https_port = '443';
$uuid = '1b29f4bd-87a4-447f-bfcd-be408822f9fb';

$realpath = realpath(dirname(__FILE__) . '/../..');

if (!defined('MODX_CORE_PATH')) {
    $modx_core_path = "$realpath/core/";
    define('MODX_CORE_PATH', $modx_core_path);
}
if (!defined('MODX_PROCESSORS_PATH')) {
    $modx_processors_path = "$realpath/core/model/modx/processors/";
    define('MODX_PROCESSORS_PATH', $modx_processors_path);
}
if (!defined('MODX_CONNECTORS_PATH')) {
    $modx_connectors_path = "$realpath/connectors/";
    $modx_connectors_url = '/connectors/';
    define('MODX_CONNECTORS_PATH', $modx_connectors_path);
    define('MODX_CONNECTORS_URL', $modx_connectors_url);
}
if (!defined('MODX_MANAGER_PATH')) {
    $modx_manager_path = "$realpath/admin/";
    $modx_manager_url = '/admin/';
    define('MODX_MANAGER_PATH', $modx_manager_path);
    define('MODX_MANAGER_URL', $modx_manager_url);
}
if (!defined('MODX_BASE_PATH')) {
    $modx_base_path = "$realpath/";
    $modx_base_url = '/';
    define('MODX_BASE_PATH', $modx_base_path);
    define('MODX_BASE_URL', $modx_base_url);
}
if (defined('PHP_SAPI') && (PHP_SAPI == "cli" || PHP_SAPI == "embed")) {
    $isSecureRequest = false;
} else {
    $isSecureRequest = ((isset ($_SERVER['HTTPS']) && strtolower($_SERVER['HTTPS']) == 'on') || $_SERVER['SERVER_PORT'] == $https_port);
}
if (!defined('MODX_URL_SCHEME')) {
    $url_scheme=  $isSecureRequest ? 'https://' : 'http://';
    define('MODX_URL_SCHEME', $url_scheme);
}
if (!defined('MODX_HTTP_HOST')) {
    if(defined('PHP_SAPI') && (PHP_SAPI == "cli" || PHP_SAPI == "embed")) {
        $http_host = 'modx.local';
        define('MODX_HTTP_HOST', $http_host);
    } else {
        $http_host = $_SERVER['HTTP_HOST'];
        if ($_SERVER['SERVER_PORT'] != 80) {
            $http_host = str_replace(':' . $_SERVER['SERVER_PORT'], '', $http_host); // remove port from HTTP_HOST
        }
        $http_host .= ($_SERVER['SERVER_PORT'] == 80 || $isSecureRequest) ? '' : ':' . $_SERVER['SERVER_PORT'];
        define('MODX_HTTP_HOST', $http_host);
    }
}
if (!defined('MODX_SITE_URL')) {
    $site_url = $url_scheme . $http_host . MODX_BASE_URL;
    define('MODX_SITE_URL', $site_url);
}
if (!defined('MODX_ASSETS_PATH')) {
    $modx_assets_path = "$realpath/assets/";
    $modx_assets_url = '/assets/';
    define('MODX_ASSETS_PATH', $modx_assets_path);
    define('MODX_ASSETS_URL', $modx_assets_url);
}
if (!defined('MODX_LOG_LEVEL_FATAL')) {
    define('MODX_LOG_LEVEL_FATAL', 0);
    define('MODX_LOG_LEVEL_ERROR', 1);
    define('MODX_LOG_LEVEL_WARN', 2);
    define('MODX_LOG_LEVEL_INFO', 3);
    define('MODX_LOG_LEVEL_DEBUG', 4);
}
if (!defined('MODX_CACHE_DISABLED')) {
    $modx_cache_disabled = true;
    define('MODX_CACHE_DISABLED', $modx_cache_disabled);
}
