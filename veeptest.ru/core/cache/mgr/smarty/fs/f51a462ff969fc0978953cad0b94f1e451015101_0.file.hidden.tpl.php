<?php /* Smarty version 3.1.27, created on 2017-10-04 11:02:03
         compiled from "/var/www/nikolay31/data/www/veeptest.ru/admin/templates/fs/element/tv/renders/input/hidden.tpl" */ ?>
<?php
/*%%SmartyHeaderCode:11932825959d4957b47dc03_35163380%%*/
if(!defined('SMARTY_DIR')) exit('no direct access allowed');
$_valid = $_smarty_tpl->decodeProperties(array (
  'file_dependency' => 
  array (
    'f51a462ff969fc0978953cad0b94f1e451015101' => 
    array (
      0 => '/var/www/nikolay31/data/www/veeptest.ru/admin/templates/fs/element/tv/renders/input/hidden.tpl',
      1 => 1506076482,
      2 => 'file',
    ),
  ),
  'nocache_hash' => '11932825959d4957b47dc03_35163380',
  'variables' => 
  array (
    'tv' => 0,
  ),
  'has_nocache_code' => false,
  'version' => '3.1.27',
  'unifunc' => 'content_59d4957b5349b7_91284303',
),false);
/*/%%SmartyHeaderCode%%*/
if ($_valid && !is_callable('content_59d4957b5349b7_91284303')) {
function content_59d4957b5349b7_91284303 ($_smarty_tpl) {

$_smarty_tpl->properties['nocache_hash'] = '11932825959d4957b47dc03_35163380';
?>
<input id="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
" name="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
" type="hidden" value="<?php echo htmlspecialchars($_smarty_tpl->tpl_vars['tv']->value->get('value'), ENT_QUOTES, 'UTF-8', true);?>
" />

<?php echo '<script'; ?>
 type="text/javascript">
// <![CDATA[

MODx.on('ready',function() {
    var fld = MODx.load({
    
        xtype: 'hidden'
        ,applyTo: 'tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
'
        ,value: '<?php echo strtr($_smarty_tpl->tpl_vars['tv']->value->get('value'), array("\\" => "\\\\", "'" => "\\'", "\"" => "\\\"", "\r" => "\\r", "\n" => "\\n", "</" => "<\/" ));?>
'
    
    });
    var p = Ext.getCmp('modx-panel-resource');
    if (p) {
        p.add(fld);
        p.doLayout();
    }
});

// ]]>
<?php echo '</script'; ?>
>
<?php }
}
?>