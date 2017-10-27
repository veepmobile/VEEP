<?php  return '/*
input:

$image1
$image2
$thumbOptionsBig
$thumbOptionsSmall
*/

if (!$image1 && !$image2) {
    return \'\';
}

if ($image1 && $image2) {
    $out = \'<a href="\' . $image1 . \'" class="zoom-gal" title="[[+pagetitle]]"><img src="\' . $modx->runSnippet(\'phpthumbof\', array(\'input\' => $image1, \'options\' => $thumbOptionsSmall)) . \'"></a>\';
    $out .= \'<a href="\' . $image2 . \'" class="zoom-gal" title="[[+pagetitle]]"><img src="\' . $modx->runSnippet(\'phpthumbof\', array(\'input\' => $image2, \'options\' => $thumbOptionsSmall)) . \'"></a>\';
    return $out;
}

$image = \'\';
if ($image1 && !$image2) {
    $image = $image1;
}
if (!$image1 && $image2) {
    $image = $image2;
}
return \'<a href="\' . $image1 . \'" class="zoom-gal" title="[[+pagetitle]]"><img src="\' . $modx->runSnippet(\'phpthumbof\', array(\'input\' => $image1, \'options\' => $thumbOptionsBig)) . \'"></a>\';
return;
';