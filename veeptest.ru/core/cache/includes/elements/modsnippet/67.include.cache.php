<?php


// v. fork-0.1 (17.11.2016)

if (!empty($options)) { 
    parse_str($options); 
} 
$eml = empty($email) ? $input : $email; 
$eml = trim($eml);
$debug = !empty($debug); 
$aslink = !(isset($aslink) && $aslink == 0); 

if (filter_var($eml, FILTER_VALIDATE_EMAIL) === false) { 
    return $debug ? 'incorrect email address' : $eml; 
} 
if (!function_exists('str2hex')) { 
    function str2hex($string) 
    { 
        $hex = ""; 
        for ($i = 0; $i < strlen($string); $i++) { 
            $hex .= (strlen(dechex(ord($string[$i]))) < 2) ? "%0" . dechex(ord($string[$i])) : '%' . dechex(ord($string[$i])); 
        } 
        return $hex; 
    } 
} 

$enc_email = $aslink ? str2hex('<a href="mailto:' . $eml . '" rel="nofollow">' . $eml . '</a>') : str2hex($eml); 
$output = ' 
    <noindex> 
        <script type="text/javascript" language="javascript"> 
            document.write(unescape(\'' . $enc_email . '\')); 
        </script> 
    </noindex>
'; 
unset($input, $eml, $enc_email); 
return $output;
return;
