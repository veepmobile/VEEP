<?php /* Smarty version 3.1.27, created on 2017-10-04 11:03:11
         compiled from "/var/www/nikolay31/data/www/veeptest.ru/core/components/mapex2/tv/input/tpl/mapex.yandexMap.tpl" */ ?>
<?php
/*%%SmartyHeaderCode:6317127459d495bf9850e1_06844167%%*/
if(!defined('SMARTY_DIR')) exit('no direct access allowed');
$_valid = $_smarty_tpl->decodeProperties(array (
  'file_dependency' => 
  array (
    '0bea3b07d09fe19dae62c5b1ea053ea303d2520c' => 
    array (
      0 => '/var/www/nikolay31/data/www/veeptest.ru/core/components/mapex2/tv/input/tpl/mapex.yandexMap.tpl',
      1 => 1506076482,
      2 => 'file',
    ),
  ),
  'nocache_hash' => '6317127459d495bf9850e1_06844167',
  'variables' => 
  array (
    'tv' => 0,
  ),
  'has_nocache_code' => false,
  'version' => '3.1.27',
  'unifunc' => 'content_59d495bfac98b5_55755462',
),false);
/*/%%SmartyHeaderCode%%*/
if ($_valid && !is_callable('content_59d495bfac98b5_55755462')) {
function content_59d495bfac98b5_55755462 ($_smarty_tpl) {

$_smarty_tpl->properties['nocache_hash'] = '6317127459d495bf9850e1_06844167';
?>
<div class="mapex-map-wrapper">
    <div id="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
Map" class="mapex-map"></div>
</div>
<div id="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
MapInput">
    <textarea id="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
" name="tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
" class="textfield" rows="5"></textarea>
</div>

<?php echo '<script'; ?>
 type="text/javascript">
    // <![CDATA[
    
    Ext.onReady(function(){
        var fld = MODx.load({
            
            xtype: 'textfield'
            ,applyTo: 'tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
'
            ,width: '99%'
            ,id: 'tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
'
            ,enableKeyEvents: true
            ,allowBlank: true
            ,value: '<?php echo $_smarty_tpl->tpl_vars['tv']->value->value;?>
'
            
            ,listeners: { 'change': { fn:MODx.fireResourceFormChange, scope:this}}
        });
        Ext.getCmp('modx-panel-resource').getForm().add(fld);
        MODx.makeDroppable(fld);
    });
    
    // ]]>
<?php echo '</script'; ?>
>


<?php echo '<script'; ?>
 type="text/javascript">
    
    if(Mapex == undefined){
        var Mapex = {};
    }

    Ext.onReady(function(){
        ymaps.ready(function() {
            // Initialize layouts
            Mapex.initLayouts();

            // Create new map
            
            var map = new Mapex.MapexMap('tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
Map', 'tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
', '<?php echo $_smarty_tpl->tpl_vars['tv']->value->value;?>
');
            map.enableControls();
            map.enableTools();

            if(!mapex2Config.showInput){
                $("#tv<?php echo $_smarty_tpl->tpl_vars['tv']->value->id;?>
" + "MapInput").hide();
            }
            

            //map.fitToViewport();
        });
    });
    
<?php echo '</script'; ?>
><?php }
}
?>