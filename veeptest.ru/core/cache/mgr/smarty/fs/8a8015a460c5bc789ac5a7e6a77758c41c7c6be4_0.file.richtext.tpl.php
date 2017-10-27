<?php /* Smarty version 3.1.27, created on 2017-10-04 11:02:03
         compiled from "/var/www/nikolay31/data/www/veeptest.ru/admin/templates/fs/element/tv/renders/input/richtext.tpl" */ ?>
<?php
/*%%SmartyHeaderCode:11536492059d4957b87fb43_06325739%%*/
if(!defined('SMARTY_DIR')) exit('no direct access allowed');
$_valid = $_smarty_tpl->decodeProperties(array (
  'file_dependency' => 
  array (
    '8a8015a460c5bc789ac5a7e6a77758c41c7c6be4' => 
    array (
      0 => '/var/www/nikolay31/data/www/veeptest.ru/admin/templates/fs/element/tv/renders/input/richtext.tpl',
      1 => 1506076482,
      2 => 'file',
    ),
  ),
  'nocache_hash' => '11536492059d4957b87fb43_06325739',
  'variables' => 
  array (
    'tv' => 0,
  ),
  'has_nocache_code' => false,
  'version' => '3.1.27',
  'unifunc' => 'content_59d4957b9084e9_41899915',
),false);
/*/%%SmartyHeaderCode%%*/
if ($_valid && !is_callable('content_59d4957b9084e9_41899915')) {
function content_59d4957b9084e9_41899915 ($_smarty_tpl) {

$_smarty_tpl->properties['nocache_hash'] = '11536492059d4957b87fb43_06325739';
?>
<textarea id="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
" name="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
" class="modx-richtext" onchange="MODx.fireResourceFormChange();"><?php echo htmlspecialchars($_smarty_tpl->tpl_vars['tv']->value->get('value'), ENT_QUOTES, 'UTF-8', true);?>
</textarea>

<?php echo '<script'; ?>
 type="text/javascript">

Ext.onReady(function() {
    
    MODx.makeDroppable(Ext.get('tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
'));
    
});
<?php echo '</script'; ?>
><?php }
}
?>