<?php  return array (
  'config' => 
  array (
    'allow_tags_in_post' => '1',
    'modRequest.class' => 'modManagerRequest',
  ),
  'aliasMap' => 
  array (
  ),
  'webLinkMap' => 
  array (
  ),
  'eventMap' => 
  array (
    'OnBeforeDocFormSave' => 
    array (
      8 => '8',
    ),
    'OnBeforeEmptyTrash' => 
    array (
      8 => '8',
    ),
    'OnChunkFormPrerender' => 
    array (
      6 => '6',
      12 => '12',
      1 => '1',
    ),
    'OnChunkFormSave' => 
    array (
      6 => '6',
    ),
    'OnDocFormPrerender' => 
    array (
      1 => '1',
      6 => '6',
      8 => '8',
      15 => '15',
      12 => '12',
    ),
    'OnDocFormRender' => 
    array (
      17 => '17',
      8 => '8',
      13 => '13',
    ),
    'OnDocFormSave' => 
    array (
      13 => '13',
      4 => '4',
      6 => '6',
    ),
    'OnDocPublished' => 
    array (
      4 => '4',
    ),
    'OnDocUnPublished' => 
    array (
      4 => '4',
    ),
    'OnFileCreateFormPrerender' => 
    array (
      1 => '1',
    ),
    'OnFileEditFormPrerender' => 
    array (
      1 => '1',
    ),
    'OnFileManagerUpload' => 
    array (
      3 => '3',
    ),
    'OnManagerPageBeforeRender' => 
    array (
      2 => '2',
      1 => '1',
      8 => '8',
    ),
    'OnManagerPageInit' => 
    array (
      8 => '8',
    ),
    'OnMODXInit' => 
    array (
      19 => '19',
    ),
    'OnPageNotFound' => 
    array (
      20 => '20',
      13 => '13',
    ),
    'OnPluginFormPrerender' => 
    array (
      1 => '1',
      12 => '12',
    ),
    'OnResourceBeforeSort' => 
    array (
      8 => '8',
    ),
    'OnResourceDelete' => 
    array (
      4 => '4',
    ),
    'OnResourceDuplicate' => 
    array (
      4 => '4',
    ),
    'OnResourceUndelete' => 
    array (
      4 => '4',
    ),
    'OnRichTextBrowserInit' => 
    array (
      2 => '2',
    ),
    'OnRichTextEditorInit' => 
    array (
      2 => '2',
    ),
    'OnRichTextEditorRegister' => 
    array (
      1 => '1',
      2 => '2',
    ),
    'OnSiteRefresh' => 
    array (
      14 => '14',
      19 => '19',
    ),
    'OnSnipFormPrerender' => 
    array (
      12 => '12',
      1 => '1',
      6 => '6',
    ),
    'OnTempFormPrerender' => 
    array (
      12 => '12',
      1 => '1',
      6 => '6',
    ),
    'OnTempFormSave' => 
    array (
      6 => '6',
    ),
    'OnTVInputPropertiesList' => 
    array (
      17 => '17',
      15 => '15',
    ),
    'OnTVInputRenderList' => 
    array (
      15 => '15',
      17 => '17',
    ),
    'OnTVOutputRenderList' => 
    array (
      17 => '17',
    ),
    'OnTVOutputRenderPropertiesList' => 
    array (
      17 => '17',
    ),
  ),
  'pluginCache' => 
  array (
    1 => 
    array (
      'id' => '1',
      'source' => '0',
      'property_preprocess' => '0',
      'name' => 'Ace',
      'description' => 'Ace code editor plugin for MODx Revolution',
      'editor_type' => '0',
      'category' => '0',
      'cache_type' => '0',
      'plugincode' => '/**
 * Ace Source Editor Plugin
 *
 * Events: OnManagerPageBeforeRender, OnRichTextEditorRegister, OnSnipFormPrerender,
 * OnTempFormPrerender, OnChunkFormPrerender, OnPluginFormPrerender,
 * OnFileCreateFormPrerender, OnFileEditFormPrerender, OnDocFormPrerender
 *
 * @author Danil Kostin <danya.postfactum(at)gmail.com>
 *
 * @package ace
 *
 * @var array $scriptProperties
 * @var Ace $ace
 */
if ($modx->event->name == \'OnRichTextEditorRegister\') {
    $modx->event->output(\'Ace\');
    return;
}

if ($modx->getOption(\'which_element_editor\', null, \'Ace\') !== \'Ace\') {
    return;
}

$ace = $modx->getService(\'ace\', \'Ace\', $modx->getOption(\'ace.core_path\', null, $modx->getOption(\'core_path\').\'components/ace/\').\'model/ace/\');
$ace->initialize();

$extensionMap = array(
    \'tpl\'   => \'text/x-smarty\',
    \'htm\'   => \'text/html\',
    \'html\'  => \'text/html\',
    \'css\'   => \'text/css\',
    \'scss\'  => \'text/x-scss\',
    \'less\'  => \'text/x-less\',
    \'svg\'   => \'image/svg+xml\',
    \'xml\'   => \'application/xml\',
    \'xsl\'   => \'application/xml\',
    \'js\'    => \'application/javascript\',
    \'json\'  => \'application/json\',
    \'php\'   => \'application/x-php\',
    \'sql\'   => \'text/x-sql\',
    \'md\'    => \'text/x-markdown\',
    \'txt\'   => \'text/plain\',
    \'twig\'  => \'text/x-twig\'
);

// Defines wether we should highlight modx tags
$modxTags = false;
switch ($modx->event->name) {
    case \'OnSnipFormPrerender\':
        $field = \'modx-snippet-snippet\';
        $mimeType = \'application/x-php\';
        break;
    case \'OnTempFormPrerender\':
        $field = \'modx-template-content\';
        $modxTags = true;

        switch (true) {
            case $modx->getOption(\'twiggy_class\'):
                $mimeType = \'text/x-twig\';
                break;
            case $modx->getOption(\'pdotools_fenom_parser\'):
                $mimeType = \'text/x-smarty\';
                break;
            default:
                $mimeType = \'text/html\';
                break;
        }

        break;
    case \'OnChunkFormPrerender\':
        $field = \'modx-chunk-snippet\';
        if ($modx->controller->chunk && $modx->controller->chunk->isStatic()) {
            $extension = pathinfo($modx->controller->chunk->getSourceFile(), PATHINFO_EXTENSION);
            $mimeType = isset($extensionMap[$extension]) ? $extensionMap[$extension] : \'text/plain\';
        } else {
            $mimeType = \'text/html\';
        }
        $modxTags = true;

        switch (true) {
            case $modx->getOption(\'twiggy_class\'):
                $mimeType = \'text/x-twig\';
                break;
            case $modx->getOption(\'pdotools_fenom_default\'):
                $mimeType = \'text/x-smarty\';
                break;
            default:
                $mimeType = \'text/html\';
                break;
        }

        break;
    case \'OnPluginFormPrerender\':
        $field = \'modx-plugin-plugincode\';
        $mimeType = \'application/x-php\';
        break;
    case \'OnFileCreateFormPrerender\':
        $field = \'modx-file-content\';
        $mimeType = \'text/plain\';
        break;
    case \'OnFileEditFormPrerender\':
        $field = \'modx-file-content\';
        $extension = pathinfo($scriptProperties[\'file\'], PATHINFO_EXTENSION);
        $mimeType = isset($extensionMap[$extension])
            ? $extensionMap[$extension]
            : \'text/plain\';
        $modxTags = $extension == \'tpl\';
        break;
    case \'OnDocFormPrerender\':
        if (!$modx->controller->resourceArray) {
            return;
        }
        $field = \'ta\';
        $mimeType = $modx->getObject(\'modContentType\', $modx->controller->resourceArray[\'content_type\'])->get(\'mime_type\');

        switch (true) {
            case $mimeType == \'text/html\' && $modx->getOption(\'twiggy_class\'):
                $mimeType = \'text/x-twig\';
                break;
            case $mimeType == \'text/html\' && $modx->getOption(\'pdotools_fenom_parser\'):
                $mimeType = \'text/x-smarty\';
                break;
        }

        if ($modx->getOption(\'use_editor\')){
            $richText = $modx->controller->resourceArray[\'richtext\'];
            $classKey = $modx->controller->resourceArray[\'class_key\'];
            if ($richText || in_array($classKey, array(\'modStaticResource\',\'modSymLink\',\'modWebLink\',\'modXMLRPCResource\'))) {
                $field = false;
            }
        }
        $modxTags = true;
        break;
    default:
        return;
}

$modxTags = (int) $modxTags;
$script = \'\';
if ($field) {
    $script .= "MODx.ux.Ace.replaceComponent(\'$field\', \'$mimeType\', $modxTags);";
}

if ($modx->event->name == \'OnDocFormPrerender\' && !$modx->getOption(\'use_editor\')) {
    $script .= "MODx.ux.Ace.replaceTextAreas(Ext.query(\'.modx-richtext\'));";
}

if ($script) {
    $modx->controller->addHtml(\'<script>Ext.onReady(function() {\' . $script . \'});</script>\');
}',
      'locked' => '0',
      'properties' => 'a:0:{}',
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => 'ace/elements/plugins/ace.plugin.php',
    ),
    2 => 
    array (
      'id' => '2',
      'source' => '0',
      'property_preprocess' => '0',
      'name' => 'CKEditor',
      'description' => 'CKEditor WYSIWYG editor plugin for MODx Revolution',
      'editor_type' => '0',
      'category' => '0',
      'cache_type' => '0',
      'plugincode' => '',
      'locked' => '0',
      'properties' => '',
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '1',
      'static_file' => 'ckeditor/elements/plugins/ckeditor.plugin.php',
    ),
    3 => 
    array (
      'id' => '3',
      'source' => '0',
      'property_preprocess' => '0',
      'name' => 'fileTranslit',
      'description' => 'This plugin transliterates file names automatically on upload.',
      'editor_type' => '0',
      'category' => '0',
      'cache_type' => '0',
      'plugincode' => '/**
 * @author Anton Andersen <anton.a.andersen@gmail.com>
 *
 * This plugin transliterates filenames on upload via MODX filemanager.
 * It should be bent to the OnFileManagerUpload event.
 * Project page: https://github.com/TriAnMan/filetranslit
 */
$currentdoc = $modx->newObject(\'modResource\');
foreach ($files as &$file) {
	if ($file[\'error\'] == 0) {
		$newName = $currentdoc->cleanAlias($file[\'name\']);

		//file rename logic
		if ($file[\'name\'] !== $newName) {
			$arDirFiles = $source->getObjectsInContainer($directory);
			foreach ($arDirFiles as &$dirFile){
				if($dirFile[\'name\']===$newName){
					//delete file if there is one with new name
					$source->removeObject($directory . $newName);
				}
			}
			//transliterate uploaded file
			$source->renameObject($directory . $file[\'name\'], $newName);
		}
	}
}',
      'locked' => '0',
      'properties' => '',
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => '',
    ),
    4 => 
    array (
      'id' => '4',
      'source' => '0',
      'property_preprocess' => '0',
      'name' => 'SimpleSearchIndexer',
      'description' => 'Automatically indexes Resources into Solr.',
      'editor_type' => '0',
      'category' => '0',
      'cache_type' => '0',
      'plugincode' => '/**
 * SimpleSearch
 *
 * Copyright 2010-11 by Shaun McCormick <shaun+sisea@modx.com>
 *
 * This file is part of SimpleSearch, a simple search component for MODx
 * Revolution. It is loosely based off of AjaxSearch for MODx Evolution by
 * coroico/kylej, minus the ajax.
 *
 * SimpleSearch is free software; you can redistribute it and/or modify it under
 * the terms of the GNU General Public License as published by the Free Software
 * Foundation; either version 2 of the License, or (at your option) any later
 * version.
 *
 * SimpleSearch is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
 * details.
 *
 * You should have received a copy of the GNU General Public License along with
 * SimpleSearch; if not, write to the Free Software Foundation, Inc., 59 Temple Place,
 * Suite 330, Boston, MA 02111-1307 USA
 *
 * @package simplesearch
 */
/**
 * Plugin to index Resources whenever they are changed, published, unpublished,
 * deleted, or undeleted.
 *
 * @var modX $modx
 * @var SimpleSearch $search
 *
 * @package simplesearch
 */

require_once $modx->getOption(\'sisea.core_path\',null,$modx->getOption(\'core_path\').\'components/simplesearch/\').\'model/simplesearch/simplesearch.class.php\';
$search = new SimpleSearch($modx,$scriptProperties);

$search->loadDriver($scriptProperties);
if (!$search->driver || (!($search->driver instanceof SimpleSearchDriverSolr) && !($search->driver instanceof SimpleSearchDriverElastic))) return;

/**
 * helper method for missing params in events
 * @param modX $modx
 * @param array $children
 * @param int $parent
 * @return boolean
 */
if (!function_exists(\'SimpleSearchGetChildren\')) {
    function SimpleSearchGetChildren(&$modx,&$children,$parent) {
        $success = false;
        $kids = $modx->getCollection(\'modResource\',array(
            \'parent\' => $parent,
        ));
        if (!empty($kids)) {
            /** @var modResource $kid */
            foreach ($kids as $kid) {
                $children[] = $kid->toArray();
                SimpleSearchGetChildren($modx,$children,$kid->get(\'id\'));
            }
        }
        return $success;
    }
}

$action = \'index\';
$resourcesToIndex = array();
switch ($modx->event->name) {
    case \'OnDocFormSave\':
        $action = \'index\';
        $resourceArray = $scriptProperties[\'resource\']->toArray();

        if ($resourceArray[\'published\'] == 1 && $resourceArray[\'deleted\'] == 0) {
            $action = \'index\';
            foreach ($_POST as $k => $v) {
                if (substr($k,0,2) == \'tv\') {
                    $id = str_replace(\'tv\',\'\',$k);
                    /** @var modTemplateVar $tv */
                    $tv = $modx->getObject(\'modTemplateVar\',$id);
                    if ($tv) {
                        $resourceArray[$tv->get(\'name\')] = $tv->renderOutput($resource->get(\'id\'));
                        $modx->log(modX::LOG_LEVEL_DEBUG,\'Indexing \'.$tv->get(\'name\').\': \'.$resourceArray[$tv->get(\'name\')]);
                    }
                    unset($resourceArray[$k]);
                }
            }
        } else {
            $action = \'removeIndex\';
        }

        unset($resourceArray[\'ta\'],$resourceArray[\'action\'],$resourceArray[\'tiny_toggle\'],$resourceArray[\'HTTP_MODAUTH\'],$resourceArray[\'modx-ab-stay\'],$resourceArray[\'resource_groups\']);
        $resourcesToIndex[] = $resourceArray;
        break;
    case \'OnDocPublished\':
        $action = \'index\';
        $resourceArray = $scriptProperties[\'resource\']->toArray();
        unset($resourceArray[\'ta\'],$resourceArray[\'action\'],$resourceArray[\'tiny_toggle\'],$resourceArray[\'HTTP_MODAUTH\'],$resourceArray[\'modx-ab-stay\'],$resourceArray[\'resource_groups\']);
        $resourcesToIndex[] = $resourceArray;
        break;
    case \'OnDocUnpublished\':
    case \'OnDocUnPublished\':
        $action = \'removeIndex\';
        $resourceArray = $scriptProperties[\'resource\']->toArray();
        unset($resourceArray[\'ta\'],$resourceArray[\'action\'],$resourceArray[\'tiny_toggle\'],$resourceArray[\'HTTP_MODAUTH\'],$resourceArray[\'modx-ab-stay\'],$resourceArray[\'resource_groups\']);
        $resourcesToIndex[] = $resourceArray;
        break;
    case \'OnResourceDuplicate\':
        $action = \'index\';
        /** @var modResource $newResource */
        $resourcesToIndex[] = $newResource->toArray();
        $children = array();
        SimpleSearchGetChildren($modx,$children,$newResource->get(\'id\'));
        foreach ($children as $child) {
            $resourcesToIndex[] = $child;
        }
        break;
    case \'OnResourceDelete\':
        $action = \'removeIndex\';
        $resourcesToIndex[] = $resource->toArray();
        $children = array();
        SimpleSearchGetChildren($modx,$children,$resource->get(\'id\'));
        foreach ($children as $child) {
            $resourcesToIndex[] = $child;
        }
        break;
    case \'OnResourceUndelete\':
        $action = \'index\';
        $resourcesToIndex[] = $resource->toArray();
        $children = array();
        SimpleSearchGetChildren($modx,$children,$resource->get(\'id\'));
        foreach ($children as $child) {
            $resourcesToIndex[] = $child;
        }
        break;
}

foreach ($resourcesToIndex as $resourceArray) {
    if (!empty($resourceArray[\'id\'])) {
        if ($action == \'index\') {
            $search->driver->index($resourceArray);
        } else if ($action == \'removeIndex\') {
            $search->driver->removeIndex($resourceArray[\'id\']);
        }
    }
}
return;',
      'locked' => '0',
      'properties' => '',
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => '',
    ),
    8 => 
    array (
      'id' => '8',
      'source' => '0',
      'property_preprocess' => '0',
      'name' => 'Collections',
      'description' => '',
      'editor_type' => '0',
      'category' => '14',
      'cache_type' => '0',
      'plugincode' => '/**
 * Collections
 *
 * DESCRIPTION
 *
 * This plugin inject JS to handle proper working of close buttons in Resource\'s panel (OnDocFormPrerender)
 * This plugin handles setting proper show_in_tree parameter (OnBeforeDocFormSave, OnResourceSort)
 *
 * @var modX $modx
 * @var array $scriptProperties
 */
$corePath = $modx->getOption(\'collections.core_path\', null, $modx->getOption(\'core_path\', null, MODX_CORE_PATH) . \'components/collections/\');
/** @var Collections $collections */
$collections = $modx->getService(
    \'collections\',
    \'Collections\',
    $corePath . \'model/collections/\',
    array(
        \'core_path\' => $corePath
    )
);

$className = \'Collections\' . $modx->event->name;

$modx->loadClass(\'CollectionsPlugin\', $collections->getOption(\'modelPath\') . \'collections/events/\', true, true);
$modx->loadClass($className, $collections->getOption(\'modelPath\') . \'collections/events/\', true, true);

if (class_exists($className)) {
    /** @var CollectionsPlugin $handler */
    $handler = new $className($modx, $scriptProperties);
    $handler->run();
}

return;',
      'locked' => '0',
      'properties' => 'a:0:{}',
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => '',
    ),
    6 => 
    array (
      'id' => '6',
      'source' => '1',
      'property_preprocess' => '0',
      'name' => 'modDevTools',
      'description' => '',
      'editor_type' => '0',
      'category' => '8',
      'cache_type' => '0',
      'plugincode' => '/**
 * modDevTools
 *
 * Copyright 2014 by Vitaly Kireev <kireevvit@gmail.com>
 *
 * @package moddevtools
 *
 * @var modX $modx
 * @var int $id
 * @var string $mode
 */

/**
 * @var modx $modx
 */
$path = $modx->getOption(\'moddevtools_core_path\',null,$modx->getOption(\'core_path\').\'components/moddevtools/\').\'model/moddevtools/\';
/**
 * @var modDevTools $devTools
 */
$devTools = $modx->getService(\'devTools\',\'modDevTools\',$path, array(\'debug\' => false));
$eventName = $modx->event->name;

switch($eventName) {
    case \'OnDocFormSave\':
        $devTools->debug(\'Start OnDocFormSave\');
        $devTools->parseContent($resource);
        break;
    case \'OnTempFormSave\':
        $devTools->debug(\'Start OnTempFormSave\');
        $devTools->parseContent($template);
        break;
    case \'OnTVFormSave\':

        break;
    case \'OnChunkFormSave\':
        $devTools->debug(\'Start OnChunkFormSave\');
        $devTools->parseContent($chunk);
        break;
    case \'OnSnipFormSave\':

        break;
    /* Add tabs */
    case \'OnDocFormPrerender\':
        if ($modx->event->name == \'OnDocFormPrerender\') {
            $devTools->getBreadCrumbs($scriptProperties);
            return;
        }
        break;

    case \'OnTempFormPrerender\':
        if ($mode == modSystemEvent::MODE_UPD) {
            $result = $devTools->outputTab(\'Template\');
        }
        break;

    case \'OnTVFormPrerender\':
        break;


    case \'OnChunkFormPrerender\':
        if ($mode == modSystemEvent::MODE_UPD) {
            $result = $devTools->outputTab(\'Chunk\');
        }
        break;

    case \'OnSnipFormPrerender\':
        if ($mode == modSystemEvent::MODE_UPD) {
            $result = $devTools->outputTab(\'Snippet\');
        }
        break;


}

if (isset($result) && $result === true)
    return;
elseif (isset($result)) {
    $modx->log(modX::LOG_LEVEL_ERROR,\'[modDevTools] An error occured. Event: \'.$eventName.\' - Error: \'.($result === false) ? \'undefined error\' : $result);
    return;
}',
      'locked' => '0',
      'properties' => '',
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => 'core/components/moddevtools/elements/plugins/plugin.moddevtools.php',
    ),
    14 => 
    array (
      'id' => '14',
      'source' => '0',
      'property_preprocess' => '0',
      'name' => 'phpThumbOfCacheManager',
      'description' => 'Handles cache cleaning when clearing the Site Cache.',
      'editor_type' => '0',
      'category' => '24',
      'cache_type' => '0',
      'plugincode' => '/*
 * Handles cache cleanup
 * pThumb
 * Copyright 2013 Jason Grant
 *
 * Please see the GitHub page for documentation or to report bugs:
 * https://github.com/oo12/phpThumbOf
 *
 * pThumb is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the Free
 * Software Foundation; either version 2 of the License, or (at your option) any
 * later version.
 *
 * pThumb is distributed in the hope that it will be useful, but WITHOUT ANY
 * WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR
 * A PARTICULAR PURPOSE. See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along with
 * phpThumbOf; if not, write to the Free Software Foundation, Inc., 59 Temple
 * Place, Suite 330, Boston, MA 02111-1307 USA
 */

if ($modx->event->name === \'OnSiteRefresh\') {
	if (!$modx->loadClass(\'pThumbCacheCleaner\', MODX_CORE_PATH . \'components/phpthumbof/model/\', true, true)) {
		$modx->log(modX::LOG_LEVEL_ERROR, \'[pThumb] Could not load pThumbCacheCleaner class.\');
		return;
	}
	static $pt_settings = array();
	$pThumb = new pThumbCacheCleaner($modx, $pt_settings, array(), true);
	$pThumb->cleanCache();
}',
      'locked' => '0',
      'properties' => NULL,
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => '',
    ),
    15 => 
    array (
      'id' => '15',
      'source' => '0',
      'property_preprocess' => '0',
      'name' => 'MIGX',
      'description' => '',
      'editor_type' => '0',
      'category' => '26',
      'cache_type' => '0',
      'plugincode' => '$corePath = $modx->getOption(\'migx.core_path\',null,$modx->getOption(\'core_path\').\'components/migx/\');
$assetsUrl = $modx->getOption(\'migx.assets_url\', null, $modx->getOption(\'assets_url\') . \'components/migx/\');
switch ($modx->event->name) {
    case \'OnTVInputRenderList\':
        $modx->event->output($corePath.\'elements/tv/input/\');
        break;
    case \'OnTVInputPropertiesList\':
        $modx->event->output($corePath.\'elements/tv/inputoptions/\');
        break;

        case \'OnDocFormPrerender\':
        $modx->controller->addCss($assetsUrl.\'css/mgr.css\');
        break; 
 
    /*          
    case \'OnTVOutputRenderList\':
        $modx->event->output($corePath.\'elements/tv/output/\');
        break;
    case \'OnTVOutputRenderPropertiesList\':
        $modx->event->output($corePath.\'elements/tv/properties/\');
        break;
    
    case \'OnDocFormPrerender\':
        $assetsUrl = $modx->getOption(\'colorpicker.assets_url\',null,$modx->getOption(\'assets_url\').\'components/colorpicker/\'); 
        $modx->regClientStartupHTMLBlock(\'<script type="text/javascript">
        Ext.onReady(function() {
            
        });
        </script>\');
        $modx->regClientStartupScript($assetsUrl.\'sources/ColorPicker.js\');
        $modx->regClientStartupScript($assetsUrl.\'sources/ColorMenu.js\');
        $modx->regClientStartupScript($assetsUrl.\'sources/ColorPickerField.js\');		
        $modx->regClientCSS($assetsUrl.\'resources/css/colorpicker.css\');
        break;
     */
}
return;',
      'locked' => '0',
      'properties' => 'a:0:{}',
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => '',
    ),
    17 => 
    array (
      'id' => '17',
      'source' => '1',
      'property_preprocess' => '0',
      'name' => 'Mapex2',
      'description' => '',
      'editor_type' => '0',
      'category' => '30',
      'cache_type' => '0',
      'plugincode' => '$corePath = $modx->getOption(\'core_path\', null, MODX_CORE_PATH).\'components/mapex2/\';
$assetsUrl = $modx->getOption(\'assets_url\', null, MODX_CORE_PATH).\'components/mapex2/\';
switch ($modx->event->name) {
    case \'OnTVInputRenderList\':
        $modx->event->output($corePath.\'tv/input/\');
        break;
    case \'OnTVOutputRenderList\':
        $modx->event->output($corePath.\'tv/output/\');
        break;
    case \'OnTVInputPropertiesList\':
        $modx->event->output($corePath.\'tv/inputoptions/\');
        break;
    case \'OnTVOutputRenderPropertiesList\':
        $modx->event->output($corePath.\'tv/properties/\');
        break;
    case \'OnDocFormRender\':
        $modx->regClientCSS($assetsUrl.\'css/mgr/mapex.default.css\');

        $jqueryUrl = $modx->getOption(\'mapex2_manager_jquery_url\', null, \'\');

        if(!empty($jqueryUrl)) {
            $modx->regClientStartupScript(\'
        <script type="text/javascript">
            if(typeof jQuery == "undefined"){
                document.write(\\\'<script type="text/javascript" src="\'.$jqueryUrl.\'" ></\\\'+\\\'script>\\\');
            };
        </script>
        \', true);
        }

        $mapCenter = $modx->getOption(\'mapex2_manager_map_default_center\', null, \'55.751565, 37.617935\');
        $mapZoom = $modx->getOption(\'mapex2_manager_map_default_zoom\', null, \'10\');
        $mapType = $modx->getOption(\'mapex2_manager_map_default_type\', null, \'yandex#map\');
        $showInput = intval($modx->getOption(\'mapex2_manager_show_input\', null, true));
        $addPlacemarkOnSearch = intval($modx->getOption(\'mapex2_manager_add_placemark_on_search\', null, false));

        $configScript = \'
        <script type="text/javascript">
            mapex2Config = {
                mapCenter: [\'.$mapCenter.\'],
                mapZoom: \'.$mapZoom.\',
                mapType: "\'.$mapType.\'",
                showInput: \'.$showInput.\',
                addPlacemarkOnSearch: \'.$addPlacemarkOnSearch.\'
            }
        </script>
        \';
        $modx->regClientStartupScript($configScript, true);

        $modx->regClientStartupScript(\'//api-maps.yandex.ru/2.0/?load=package.full&;lang=ru-RU\');

        //$modx->regClientStartupScript($assetsUrl.\'js/mgr/mapex.ym.js\');

        $modx->regClientStartupScript($assetsUrl.\'js/mgr/mapex.init.js\');
        $modx->regClientStartupScript($assetsUrl.\'js/mgr/mapex.storage.js\');
        $modx->regClientStartupScript($assetsUrl.\'js/mgr/mapex.layouts.js\');
        $modx->regClientStartupScript($assetsUrl.\'js/mgr/mapex.placemark.js\');
        $modx->regClientStartupScript($assetsUrl.\'js/mgr/mapex.line.js\');
        $modx->regClientStartupScript($assetsUrl.\'js/mgr/mapex.polygon.js\');
        $modx->regClientStartupScript($assetsUrl.\'js/mgr/mapex.route.js\');
        $modx->regClientStartupScript($assetsUrl.\'js/mgr/mapex.map.js\');

        break;
}',
      'locked' => '0',
      'properties' => 'a:0:{}',
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => 'core/components/mapex2/elements/plugins/plugin.mapex2.php',
    ),
    12 => 
    array (
      'id' => '12',
      'source' => '1',
      'property_preprocess' => '0',
      'name' => 'tagElementPlugin',
      'description' => '',
      'editor_type' => '0',
      'category' => '22',
      'cache_type' => '0',
      'plugincode' => 'switch ($modx->event->name) {
    case \'OnDocFormPrerender\':
        $field = \'modx-snippet-snippet\';
        $panel = \'\';
        break;
    case \'OnTempFormPrerender\':
        $field = \'modx-template-content\';
        $panel = \'modx-panel-template\';
        break;
    case \'OnChunkFormPrerender\':
        $field = \'modx-chunk-snippet\';
        $panel = \'modx-panel-chunk\';
        break;
    case \'OnSnipFormPrerender\':
        $field = \'modx-snippet-snippet\';
        $panel = \'modx-panel-snippet\';
        break;
    case \'OnPluginFormPrerender\':
        $field = \'modx-plugin-plugincode\';
        $panel = \'modx-panel-plugin\';
        break;
    default:
        return;
}
if (!empty($field)) {
    $modx->controller->addLexiconTopic(\'core:chunk\');
    $modx->controller->addLexiconTopic(\'core:snippet\');
    $modx->controller->addLexiconTopic(\'tagelementplugin:default\');
    $jsUrl = $modx->getOption(\'tagelementplugin_assets_url\', null, $modx->getOption(\'assets_url\') . \'components/tagelementplugin/\').\'js/mgr/\';
    /** @var modManagerController */
    $modx->controller->addLastJavascript($jsUrl.\'tagelementplugin.js\');
    $modx->controller->addLastJavascript($jsUrl.\'dialogs.js\');
    $usingFenon = $modx->getOption(\'pdotools_fenom_parser\',null,false) ? \'true\' : \'false\';
    $_html = "<script type=\\"text/javascript\\">\\n";
    $_html .= "\\tvar tagElPlugin = {};\\n";
    $_html .= "\\ttagElPlugin.config = {\\n";
    $_html .= "\\t\\tpanel : \'{$panel}\',\\n" ;
    $_html .= "\\t\\tfield : \'{$field}\',\\n" ;
    $_html .= "\\t\\tparent : [],\\n" ;
    $_html .= "\\t\\tkeys : {\\n\\t\\t\\tquickEditor :". $modx->getOption(\'tagelementplugin_quick_editor_keys\',null,\'\') . ",\\n" ;
    $_html .= "\\t\\t\\telementEditor :". $modx->getOption(\'tagelementplugin_element_editor_keys\',null,\'\') . ",\\n" ;
    $_html .= "\\t\\t\\tchunkEditor :". $modx->getOption(\'tagelementplugin_chunk_editor_keys\',null,\'\') . ",\\n" ;
    $_html .= "\\t\\t\\tquickChunkEditor :". $modx->getOption(\'tagelementplugin_quick_chunk_editor_keys\',null,\'\') . ",\\n" ;
    $_html .= "\\t\\t\\telementProperties :". $modx->getOption(\'tagelementplugin_element_prop_keys\',null,\'\') . "\\n\\t\\t},\\n" ;
    $_html .= "\\t\\tusing_fenom : {$usingFenon},\\n" ;
    $_html .= "\\t\\teditor : \'".$modx->getOption(\'which_element_editor\')."\',\\n" ;
    $_html .= "\\t\\tconnector_url : \'". $modx->getOption(\'tagelementplugin_assets_url\', null, $modx->getOption(\'assets_url\') . "components/tagelementplugin/")."connector.php\'\\n";
    $_html .= "\\t};\\n";
    $_html .= "</script>";
    $modx->controller->addHtml($_html);
}',
      'locked' => '0',
      'properties' => NULL,
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => 'core/components/tagelementplugin/elements/plugins/plugin.tagelementplugin.php',
    ),
    13 => 
    array (
      'id' => '13',
      'source' => '0',
      'property_preprocess' => '0',
      'name' => 'Redirector',
      'description' => 'Handles site redirects.',
      'editor_type' => '0',
      'category' => '0',
      'cache_type' => '0',
      'plugincode' => '/**
 * @package redirector
 *
 * @var modX|xPDO $modx
 * @var array $scriptProperties
 * @var modResource $resource
 * @var string $mode
 */

/* load redirector class */
$corePath = $modx->getOption(\'redirector.core_path\', $scriptProperties, $modx->getOption(\'core_path\') . \'components/redirector/\');
$redirector = $modx->getService(\'redirector\', \'Redirector\', $corePath . \'model/redirector/\', $scriptProperties);
if (!($redirector instanceof Redirector)) {
    return \'\';
}

$eventName = $modx->event->name;
switch ($eventName) {
    case \'OnPageNotFound\':

        /* handle redirects */
        $search = rawurldecode($_SERVER[\'REQUEST_URI\']);
        $baseUrl = $modx->getOption(\'base_url\', null, MODX_BASE_URL);
        if (!empty($baseUrl) && $baseUrl != \'/\' && $baseUrl != \' \' && $baseUrl != \'/\' . $modx->context->get(\'key\') . \'/\') {
            $search = str_replace($baseUrl, \'\', $search);
        }

        $search = ltrim($search, \'/\');
        if (!empty($search)) {

            /** @var modRedirect $redirect */
            $redirect = $modx->getObject(\'modRedirect\', array(
                "(`modRedirect`.`pattern` = \'" . $search . "\')",
                "(`modRedirect`.`context_key` = \'" . $modx->context->get(\'key\') . "\' OR `modRedirect`.`context_key` IS NULL OR `modRedirect`.`context_key` = \'\')",
                \'active\' => true,
            ));

            // when not found, check a REGEX record..
            // need to separate this one because of some \'alias.html > target.html\' vs. \'best-alias.html > best-target.html\' issues...
            if (empty($redirect) || !is_object($redirect)) {
                $c = $modx->newQuery(\'modRedirect\');
                $c->where(array(
                    "(`modRedirect`.`pattern` = \'" . $search . "\' OR \'" . $search . "\' REGEXP `modRedirect`.`pattern` OR \'" . $search . "\' REGEXP CONCAT(\'^\', `modRedirect`.`pattern`, \'$\'))",
                    "(`modRedirect`.`context_key` = \'" . $modx->context->get(\'key\') . "\' OR `modRedirect`.`context_key` IS NULL OR `modRedirect`.`context_key` = \'\')",
                    \'active\' => true,
                ));
                $redirect = $modx->getObject(\'modRedirect\', $c);
            }

            if (!empty($redirect) && is_object($redirect)) {

                /** @var modContext $context */
                $context = $redirect->getOne(\'Context\');
                if (empty($context) || !($context instanceof modContext)) {
                    $context = $modx->context;
                }

                $target = $redirect->get(\'target\');
                $modx->parser->processElementTags(\'\', $target, true, true);

                if ($target != $modx->resourceIdentifier && $target != $search) {
                    if (strpos($target, \'$\') !== false) {
                        $pattern = $redirect->get(\'pattern\');
                        $target = preg_replace(\'/\' . $pattern . \'/\', $target, $search);
                    }
                    if (!strpos($target, \'://\')) {
                        $target = rtrim($context->getOption(\'site_url\'), \'/\') . \'/\' . (($target == \'/\') ? \'\' : ltrim($target, \'/\'));
                    }
                    $modx->log(modX::LOG_LEVEL_INFO, \'Redirector plugin redirecting request for \' . $search . \' to \' . $target);

                    $redirect->registerTrigger();

                    $options = array(\'responseCode\' => \'HTTP/1.1 301 Moved Permanently\');
                    $modx->sendRedirect($target, $options);
                }
            }
        }

        break;

    case \'OnDocFormRender\':

        $track_uri_updates = (boolean)$modx->getOption(\'redirector.track_uri_updates\', null, 1);
        $track_uri_updates = (in_array($track_uri_updates, array(false, \'false\', 0, \'0\', \'no\', \'n\'), true)) ? false : true;

        if ($mode == \'upd\' && $track_uri_updates) {
            $_SESSION[\'modx_resource_uri\'] = $resource->get(\'uri\');
        }

        break;

    case \'OnDocFormSave\':

        /* if uri has changed, add to redirects */
        $track_uri_updates = $modx->getOption(\'redirector.track_uri_updates\', null, 1);
        $track_uri_updates = (in_array($track_uri_updates, array(false, \'false\', 0, \'0\', \'no\', \'n\'), true)) ? false : true;
        $context_key = $resource->get(\'context_key\');
        $new_uri = $resource->get(\'uri\');

        if ($mode == \'upd\' && $track_uri_updates && !empty($_SESSION[\'modx_resource_uri\'])) {

            $old_uri = $_SESSION[\'modx_resource_uri\'];
            if ($old_uri != $new_uri) {

                /* uri changed */
                $redirect = $modx->getObject(\'modRedirect\', array(
                    \'pattern\' => $old_uri,
                    \'context_key\' => $context_key,
                    \'active\' => true
                ));
                if (empty($redirect)) {

                    /* no record for old uri */
                    $new_redirect = $modx->newObject(\'modRedirect\');
                    $new_redirect->fromArray(array(
                        \'pattern\' => $old_uri,
                        \'target\' => \'[[~\' . $resource->get(\'id\') . \']]\',
                        \'context_key\' => $context_key,
                        \'active\' => true,
                    ));

                    if ($new_redirect->save() == false) {
                        return $modx->error->failure($modx->lexicon(\'redirector.redirect_err_save\'));
                    }
                }
            }

            $_SESSION[\'modx_resource_uri\'] = $new_uri;
        }

        break;
}

return \'\';',
      'locked' => '0',
      'properties' => NULL,
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => '',
    ),
    19 => 
    array (
      'id' => '19',
      'source' => '1',
      'property_preprocess' => '0',
      'name' => 'pdoTools',
      'description' => '',
      'editor_type' => '0',
      'category' => '7',
      'cache_type' => '0',
      'plugincode' => '/** @var modX $modx */
switch ($modx->event->name) {

    case \'OnMODXInit\':
        $fqn = $modx->getOption(\'pdoTools.class\', null, \'pdotools.pdotools\', true);
        $path = $modx->getOption(\'pdotools_class_path\', null, MODX_CORE_PATH . \'components/pdotools/model/\', true);
        $modx->loadClass($fqn, $path, false, true);

        $fqn = $modx->getOption(\'pdoFetch.class\', null, \'pdotools.pdofetch\', true);
        $path = $modx->getOption(\'pdofetch_class_path\', null, MODX_CORE_PATH . \'components/pdotools/model/\', true);
        $modx->loadClass($fqn, $path, false, true);
        break;

    case \'OnSiteRefresh\':
        /** @var pdoTools $pdoTools */
        if ($pdoTools = $modx->getService(\'pdoTools\')) {
            if ($pdoTools->clearFileCache()) {
                $modx->log(modX::LOG_LEVEL_INFO, $modx->lexicon(\'refresh_default\') . \': pdoTools\');
            }
        }
        break;
}',
      'locked' => '0',
      'properties' => NULL,
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => 'core/components/pdotools/elements/plugins/plugin.pdotools.php',
    ),
    20 => 
    array (
      'id' => '20',
      'source' => '1',
      'property_preprocess' => '0',
      'name' => 'BannerYClickout',
      'description' => 'Handle ad clicks',
      'editor_type' => '0',
      'category' => '35',
      'cache_type' => '0',
      'plugincode' => 'if ($modx->event->name == \'OnPageNotFound\') {
	$bannery_click = $modx->getOption(\'bannery_click\', null, \'bannerclick\', true);
	if (preg_match(\'/\'.$bannery_click.\'\\/([0-9]+)/\', $_SERVER[\'REQUEST_URI\'], $matches)) {
		$modx->addPackage(\'bannery\', $modx->getOption(\'core_path\') . \'components/bannery/model/\');

		$id = $matches[1];
		$c = $modx->newQuery(\'byAd\');
		$c->select(\'`byAd`.`id`, `Position`.`position`, `byAd`.`url`\');
		$c->leftJoin(\'byAdPosition\', \'Position\', \'Position.ad = byAd.id\');
		$c->where(array(\'Position.id\' => $id));
		if ($ad = $modx->getObject(\'byAd\', $c)) {
			$key = array(
				\'ad\' => $ad->get(\'id\'),
				\'position\' => $ad->get(\'position\'),
				\'ip\' => $_SERVER[\'REMOTE_ADDR\'],
				\'clickdate:LIKE\' => date(\'Y-m-d\') . \'%\'
			);
			if (!$modx->getCount(\'byClick\', $key)) {
				$click = $modx->newObject(\'byClick\', array(
					\'ad\' => $ad->get(\'id\'),
					\'position\' => $ad->get(\'position\'),
					\'clickdate\' => date(\'Y-m-d H:i:s\'),
					\'referrer\' => $_SERVER[\'HTTP_REFERER\'],
					\'ip\' => $_SERVER[\'REMOTE_ADDR\']
				));
				$click->save();
			}
			$url = $ad->get(\'url\');
			$chunk = $modx->newObject(\'modChunk\');
			$chunk->set(\'name\', \'banneryPosition\' . $id);
			$chunk->setContent($url);
			$url = $chunk->process($_GET);

			$modx->sendRedirect($url);
		}
	}
}',
      'locked' => '0',
      'properties' => NULL,
      'disabled' => '0',
      'moduleguid' => '',
      'static' => '0',
      'static_file' => 'core/components/bannery/elements/plugins/plugin.banneryclickout.php',
    ),
  ),
  'policies' => 
  array (
    'modAccessContext' => 
    array (
      'mgr' => 
      array (
        0 => 
        array (
          'principal' => 1,
          'authority' => 0,
          'policy' => 
          array (
            'about' => true,
            'access_permissions' => true,
            'actions' => true,
            'change_password' => true,
            'change_profile' => true,
            'charsets' => true,
            'class_map' => true,
            'components' => true,
            'content_types' => true,
            'countries' => true,
            'create' => true,
            'credits' => true,
            'customize_forms' => true,
            'dashboards' => true,
            'database' => true,
            'database_truncate' => true,
            'delete_category' => true,
            'delete_chunk' => true,
            'delete_context' => true,
            'delete_document' => true,
            'delete_eventlog' => true,
            'delete_plugin' => true,
            'delete_propertyset' => true,
            'delete_role' => true,
            'delete_snippet' => true,
            'delete_template' => true,
            'delete_tv' => true,
            'delete_user' => true,
            'directory_chmod' => true,
            'directory_create' => true,
            'directory_list' => true,
            'directory_remove' => true,
            'directory_update' => true,
            'edit_category' => true,
            'edit_chunk' => true,
            'edit_context' => true,
            'edit_document' => true,
            'edit_locked' => true,
            'edit_plugin' => true,
            'edit_propertyset' => true,
            'edit_role' => true,
            'edit_snippet' => true,
            'edit_template' => true,
            'edit_tv' => true,
            'edit_user' => true,
            'element_tree' => true,
            'empty_cache' => true,
            'error_log_erase' => true,
            'error_log_view' => true,
            'export_static' => true,
            'file_create' => true,
            'file_list' => true,
            'file_manager' => true,
            'file_remove' => true,
            'file_tree' => true,
            'file_update' => true,
            'file_upload' => true,
            'file_view' => true,
            'flush_sessions' => true,
            'frames' => true,
            'help' => true,
            'home' => true,
            'import_static' => true,
            'languages' => true,
            'lexicons' => true,
            'list' => true,
            'load' => true,
            'logout' => true,
            'logs' => true,
            'menus' => true,
            'menu_reports' => true,
            'menu_security' => true,
            'menu_site' => true,
            'menu_support' => true,
            'menu_system' => true,
            'menu_tools' => true,
            'menu_user' => true,
            'messages' => true,
            'namespaces' => true,
            'new_category' => true,
            'new_chunk' => true,
            'new_context' => true,
            'new_document' => true,
            'new_document_in_root' => true,
            'new_plugin' => true,
            'new_propertyset' => true,
            'new_role' => true,
            'new_snippet' => true,
            'new_static_resource' => true,
            'new_symlink' => true,
            'new_template' => true,
            'new_tv' => true,
            'new_user' => true,
            'new_weblink' => true,
            'packages' => true,
            'policy_delete' => true,
            'policy_edit' => true,
            'policy_new' => true,
            'policy_save' => true,
            'policy_template_delete' => true,
            'policy_template_edit' => true,
            'policy_template_new' => true,
            'policy_template_save' => true,
            'policy_template_view' => true,
            'policy_view' => true,
            'property_sets' => true,
            'providers' => true,
            'publish_document' => true,
            'purge_deleted' => true,
            'remove' => true,
            'remove_locks' => true,
            'resource_duplicate' => true,
            'resourcegroup_delete' => true,
            'resourcegroup_edit' => true,
            'resourcegroup_new' => true,
            'resourcegroup_resource_edit' => true,
            'resourcegroup_resource_list' => true,
            'resourcegroup_save' => true,
            'resourcegroup_view' => true,
            'resource_quick_create' => true,
            'resource_quick_update' => true,
            'resource_tree' => true,
            'save' => true,
            'save_category' => true,
            'save_chunk' => true,
            'save_context' => true,
            'save_document' => true,
            'save_plugin' => true,
            'save_propertyset' => true,
            'save_role' => true,
            'save_snippet' => true,
            'save_template' => true,
            'save_tv' => true,
            'save_user' => true,
            'search' => true,
            'settings' => true,
            'sources' => true,
            'source_delete' => true,
            'source_edit' => true,
            'source_save' => true,
            'source_view' => true,
            'steal_locks' => true,
            'tree_show_element_ids' => true,
            'tree_show_resource_ids' => true,
            'undelete_document' => true,
            'unlock_element_properties' => true,
            'unpublish_document' => true,
            'usergroup_delete' => true,
            'usergroup_edit' => true,
            'usergroup_new' => true,
            'usergroup_save' => true,
            'usergroup_user_edit' => true,
            'usergroup_user_list' => true,
            'usergroup_view' => true,
            'view' => true,
            'view_category' => true,
            'view_chunk' => true,
            'view_context' => true,
            'view_document' => true,
            'view_element' => true,
            'view_eventlog' => true,
            'view_offline' => true,
            'view_plugin' => true,
            'view_propertyset' => true,
            'view_role' => true,
            'view_snippet' => true,
            'view_sysinfo' => true,
            'view_template' => true,
            'view_tv' => true,
            'view_unpublished' => true,
            'view_user' => true,
            'workspaces' => true,
          ),
        ),
        1 => 
        array (
          'principal' => 2,
          'authority' => 0,
          'policy' => 
          array (
            'load' => true,
            'list' => true,
            'view' => true,
          ),
        ),
        2 => 
        array (
          'principal' => 2,
          'authority' => 0,
          'policy' => 
          array (
            'about' => false,
            'access_permissions' => false,
            'actions' => false,
            'change_password' => true,
            'change_profile' => true,
            'charsets' => false,
            'class_map' => true,
            'components' => true,
            'content_types' => false,
            'countries' => true,
            'create' => true,
            'credits' => false,
            'customize_forms' => false,
            'dashboards' => true,
            'database' => true,
            'database_truncate' => false,
            'delete_category' => false,
            'delete_chunk' => true,
            'delete_context' => false,
            'delete_document' => true,
            'delete_eventlog' => false,
            'delete_plugin' => false,
            'delete_propertyset' => false,
            'delete_role' => false,
            'delete_snippet' => false,
            'delete_template' => true,
            'delete_tv' => true,
            'delete_user' => true,
            'directory_chmod' => true,
            'directory_create' => true,
            'directory_list' => true,
            'directory_remove' => true,
            'directory_update' => true,
            'edit_category' => false,
            'edit_chunk' => true,
            'edit_context' => false,
            'edit_document' => true,
            'edit_locked' => true,
            'edit_plugin' => false,
            'edit_propertyset' => true,
            'edit_role' => false,
            'edit_snippet' => false,
            'edit_template' => true,
            'edit_tv' => true,
            'edit_user' => true,
            'element_tree' => true,
            'empty_cache' => true,
            'error_log_erase' => false,
            'error_log_view' => true,
            'events' => false,
            'export_static' => false,
            'file_create' => true,
            'file_list' => true,
            'file_manager' => true,
            'file_remove' => true,
            'file_tree' => true,
            'file_update' => true,
            'file_upload' => true,
            'file_view' => true,
            'flush_sessions' => false,
            'frames' => true,
            'help' => true,
            'home' => true,
            'import_static' => false,
            'languages' => true,
            'lexicons' => true,
            'list' => true,
            'load' => true,
            'logout' => true,
            'logs' => false,
            'menus' => false,
            'menu_reports' => true,
            'menu_security' => false,
            'menu_site' => true,
            'menu_support' => true,
            'menu_system' => false,
            'menu_tools' => true,
            'menu_user' => true,
            'messages' => false,
            'namespaces' => false,
            'new_category' => false,
            'new_chunk' => true,
            'new_context' => false,
            'new_document' => true,
            'new_document_in_root' => true,
            'new_plugin' => false,
            'new_propertyset' => false,
            'new_role' => false,
            'new_snippet' => true,
            'new_static_resource' => false,
            'new_symlink' => false,
            'new_template' => true,
            'new_tv' => true,
            'new_user' => true,
            'new_weblink' => true,
            'packages' => false,
            'policy_delete' => false,
            'policy_edit' => false,
            'policy_new' => false,
            'policy_save' => false,
            'policy_template_delete' => false,
            'policy_template_edit' => false,
            'policy_template_new' => false,
            'policy_template_save' => false,
            'policy_template_view' => false,
            'policy_view' => false,
            'property_sets' => true,
            'providers' => false,
            'publish_document' => true,
            'purge_deleted' => true,
            'remove' => true,
            'remove_locks' => true,
            'resourcegroup_delete' => false,
            'resourcegroup_edit' => false,
            'resourcegroup_new' => false,
            'resourcegroup_resource_edit' => false,
            'resourcegroup_resource_list' => false,
            'resourcegroup_save' => false,
            'resourcegroup_view' => false,
            'resource_duplicate' => false,
            'resource_quick_create' => true,
            'resource_quick_update' => true,
            'resource_tree' => true,
            'save' => true,
            'save_category' => false,
            'save_chunk' => true,
            'save_context' => false,
            'save_document' => true,
            'save_plugin' => false,
            'save_propertyset' => true,
            'save_role' => false,
            'save_snippet' => false,
            'save_template' => true,
            'save_tv' => true,
            'save_user' => true,
            'search' => true,
            'settings' => false,
            'sources' => false,
            'source_delete' => false,
            'source_edit' => false,
            'source_save' => false,
            'source_view' => true,
            'steal_locks' => false,
            'tree_show_element_ids' => true,
            'tree_show_resource_ids' => true,
            'undelete_document' => true,
            'unlock_element_properties' => false,
            'unpublish_document' => true,
            'usergroup_delete' => false,
            'usergroup_edit' => false,
            'usergroup_new' => false,
            'usergroup_save' => false,
            'usergroup_user_edit' => false,
            'usergroup_user_list' => true,
            'usergroup_view' => true,
            'view' => true,
            'view_category' => true,
            'view_chunk' => true,
            'view_context' => false,
            'view_document' => true,
            'view_element' => true,
            'view_eventlog' => false,
            'view_offline' => true,
            'view_plugin' => false,
            'view_propertyset' => true,
            'view_role' => false,
            'view_snippet' => false,
            'view_sysinfo' => false,
            'view_template' => true,
            'view_tv' => true,
            'view_unpublished' => true,
            'view_user' => true,
            'workspaces' => false,
          ),
        ),
      ),
    ),
  ),
);