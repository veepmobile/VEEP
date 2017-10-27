<?php /* Smarty version 3.1.27, created on 2017-10-04 11:03:10
         compiled from "/var/www/nikolay31/data/www/veeptest.ru/admin/templates/fs/element/tv/renders/input/email.tpl" */ ?>
<?php
/*%%SmartyHeaderCode:120231299559d495bec20b89_83687784%%*/
if(!defined('SMARTY_DIR')) exit('no direct access allowed');
$_valid = $_smarty_tpl->decodeProperties(array (
  'file_dependency' => 
  array (
    '2c2e4083feb6c7f9ce4a26eb26e368ce0797bf4b' => 
    array (
      0 => '/var/www/nikolay31/data/www/veeptest.ru/admin/templates/fs/element/tv/renders/input/email.tpl',
      1 => 1506076482,
      2 => 'file',
    ),
  ),
  'nocache_hash' => '120231299559d495bec20b89_83687784',
  'variables' => 
  array (
    'tv' => 0,
    'style' => 0,
    'params' => 0,
  ),
  'has_nocache_code' => false,
  'version' => '3.1.27',
  'unifunc' => 'content_59d495bedcc285_51654247',
),false);
/*/%%SmartyHeaderCode%%*/
if ($_valid && !is_callable('content_59d495bedcc285_51654247')) {
function content_59d495bedcc285_51654247 ($_smarty_tpl) {

$_smarty_tpl->properties['nocache_hash'] = '120231299559d495bec20b89_83687784';
?>
<input id="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
" name="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
"
	type="text" class="textfield"
	value="<?php echo htmlspecialchars($_smarty_tpl->tpl_vars['tv']->value->get('value'), ENT_QUOTES, 'UTF-8', true);?>
"
	<?php echo $_smarty_tpl->tpl_vars['style']->value;?>

	tvtype="<?php echo $_smarty_tpl->tpl_vars['tv']->value->type;?>
"
/>

<?php echo '<script'; ?>
 type="text/javascript">
// <![CDATA[

Ext.onReady(function() {
    var fld = MODx.load({
    
        xtype: 'textfield'
        ,applyTo: 'tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
'
        ,width: 400
        ,vtype: 'email'
        ,enableKeyEvents: true
        ,msgTarget: 'under'
        ,allowBlank: <?php if ($_smarty_tpl->tpl_vars['params']->value['allowBlank'] == 1 || $_smarty_tpl->tpl_vars['params']->value['allowBlank'] == 'true') {?>true<?php } else { ?>false<?php }?>
        <?php if ($_smarty_tpl->tpl_vars['params']->value['maxLength'] != '' && $_smarty_tpl->tpl_vars['params']->value['maxLength'] > 0) {
if ($_smarty_tpl->tpl_vars['params']->value['minLength'] != '' && $_smarty_tpl->tpl_vars['params']->value['minLength'] >= 0 && $_smarty_tpl->tpl_vars['params']->value['maxLength'] > $_smarty_tpl->tpl_vars['params']->value['minLength']) {?>,maxLength: <?php echo sprintf("%d",$_smarty_tpl->tpl_vars['params']->value['maxLength']);
}?> <?php }?> 
        <?php if ($_smarty_tpl->tpl_vars['params']->value['minLength'] != '' && $_smarty_tpl->tpl_vars['params']->value['minLength'] >= 0) {?>,minLength: <?php echo sprintf("%d",$_smarty_tpl->tpl_vars['params']->value['minLength']);
}?> 
    
        ,listeners: { 'keydown': { fn:MODx.fireResourceFormChange, scope:this}}
    });
    MODx.makeDroppable(fld);
    Ext.getCmp('modx-panel-resource').getForm().add(fld);
});

// ]]>
<?php echo '</script'; ?>
>
<?php }
}
?>