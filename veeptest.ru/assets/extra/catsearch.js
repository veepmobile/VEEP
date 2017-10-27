var selected = [];

$().ready(function(){
	var sss = '';
	var level = 0;
	var defval= 0;
	$('#qwe').children().each(function(){
		var arr = $.trim($(this).attr('label')).split('::');
		if($(this).attr('rel')=="selected")
			selected.push($(this).val());
		for (var i = level; i >= arr.length; i--) {
			sss += '</div>';
		}

		if (defval == 0) defval = $(this).val();
		name = $.trim($(this).attr('label').replace(/::/g,''));
		sss += '<div name="'+name+'" id="cat_tree_'+$(this).val()+'" value="'+$(this).val()+'" class="cat_tree_element" style="border: solid 1px black; margin: 5px;">'+name;
	
		level = arr.length;
	});

	for (var i = level; i >= 1; i--) {
		sss += '</div>';
	}

	if ( $('#sel_place').length == 0 ) $('#qwe').replaceWith('<div style="display: none;" id="cat_tree_0">'+sss+'</div><input type="hidden" name="qwe" id="qwe" value="'+defval+'"><div id="sel_place"></div>');
	$('#sel_place').html(create_select(0));
	selected = [];
});

function create_select(parent, text){
	var createVal=null;
	var createPar=null;
	var AllCat = '[Все категории]';
	if (text != undefined){
		if ( parent == AllCat )
			$('#qwe').val(text);
		else
			$('#qwe').val(parent);
	}

	if ( $('#cat_tree_'+parent).children().length > 0 ){
		var temp = 0;
		var res = '<select onChange="$(this).nextAll().remove();$(this).after(create_select($(this).val(), '+parent+'));" style="width: 226px; margin-bottom: 5px;';
		if ( $('#cat_tree_'+parent).children().length == 1 && parent == 0 ) res += ' display: none; ';
		res += '" class="catSelector">';
		if (parent != 0) res += '<option>'+AllCat+'</option>';
		$('#cat_tree_'+parent).children().each(function(){
			if($(this).attr('value') in oc(selected)) {
				val = $(this).attr('value');
				createVal = val;
				createPar=parent;
				res += '<option value="'+$(this).attr('value')+'" selected="selected">'+$(this).attr('name')+'</option>';
			} else
				res += '<option value="'+$(this).attr('value')+'">'+$(this).attr('name')+'</option>';
			temp = $(this).attr('value');
		});
		res += '</select>';
		if(createVal != null && createPar != null) {
			res += create_select(createVal, createPar);
			createVal=null;
			createPar=null;
		}else if ( $('#cat_tree_'+parent).children().length == 1 )
			res += create_select(temp);
		
		return res;
	} else return '';
};

function oc(a)
{
  var o = {};
  for(var i=0;i<a.length;i++)
  {
    o[a[i]]='';
  }
  return o;
}

function is_array(input){
	return typeof(input)=='object'&&(input instanceof Array);
}

