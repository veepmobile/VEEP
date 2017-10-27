<?php /* Smarty version 3.1.27, created on 2017-09-22 16:48:14
         compiled from "/var/www/nikolay31/data/www/veeptest.ru/admin/templates/fs/welcome.tpl" */ ?>
<?php
/*%%SmartyHeaderCode:30694706359c5149ee8ea14_12024336%%*/
if(!defined('SMARTY_DIR')) exit('no direct access allowed');
$_valid = $_smarty_tpl->decodeProperties(array (
  'file_dependency' => 
  array (
    '0752cb100b9b38f0271c025a2926f9453197a09e' => 
    array (
      0 => '/var/www/nikolay31/data/www/veeptest.ru/admin/templates/fs/welcome.tpl',
      1 => 1506076482,
      2 => 'file',
    ),
  ),
  'nocache_hash' => '30694706359c5149ee8ea14_12024336',
  'variables' => 
  array (
    'dashboard' => 0,
  ),
  'has_nocache_code' => false,
  'version' => '3.1.27',
  'unifunc' => 'content_59c5149ee927f8_95900384',
),false);
/*/%%SmartyHeaderCode%%*/
if ($_valid && !is_callable('content_59c5149ee927f8_95900384')) {
function content_59c5149ee927f8_95900384 ($_smarty_tpl) {

$_smarty_tpl->properties['nocache_hash'] = '30694706359c5149ee8ea14_12024336';
?>
<div id="modx-panel-welcome-div"></div>

<div id="modx-dashboard" class="dashboard">
<?php echo $_smarty_tpl->tpl_vars['dashboard']->value;?>

</div><?php }
}
?>