<?php  return array (
  'resourceClass' => 'modDocument',
  'resource' => 
  array (
    'id' => 4,
    'type' => 'document',
    'contentType' => 'text/html',
    'pagetitle' => 'Ошибка 404',
    'longtitle' => '',
    'description' => '',
    'alias' => 'oshibka-404',
    'link_attributes' => '',
    'published' => 1,
    'pub_date' => 0,
    'unpub_date' => 0,
    'parent' => 0,
    'isfolder' => 0,
    'introtext' => '',
    'content' => '<p>Страница не найдена. Возможно сменился её адрес. Попробуйте начать с <a href="/">главной страницы.</a></p>
',
    'richtext' => 1,
    'template' => 1,
    'menuindex' => 6,
    'searchable' => 0,
    'cacheable' => 1,
    'createdby' => 1,
    'createdon' => 1418657882,
    'editedby' => 1,
    'editedon' => 1501661040,
    'deleted' => 0,
    'deletedon' => 0,
    'deletedby' => 0,
    'publishedon' => 1418657880,
    'publishedby' => 1,
    'menutitle' => '',
    'donthit' => 0,
    'privateweb' => 0,
    'privatemgr' => 0,
    'content_dispo' => 0,
    'hidemenu' => 1,
    'class_key' => 'modDocument',
    'context_key' => 'web',
    'content_type' => 1,
    'uri' => 'oshibka-404.html',
    'uri_override' => 0,
    'hide_children_in_tree' => 0,
    'show_in_tree' => 1,
    'properties' => '',
    'block-image1' => 
    array (
      0 => 'block-image1',
      1 => 'images/no-photo.png',
      2 => 'default',
      3 => NULL,
      4 => 'image',
    ),
    'block-image2' => 
    array (
      0 => 'block-image2',
      1 => '',
      2 => 'default',
      3 => NULL,
      4 => 'image',
    ),
    'col2' => 
    array (
      0 => 'col2',
      1 => '',
      2 => 'default',
      3 => NULL,
      4 => 'richtext',
    ),
    'title2' => 
    array (
      0 => 'title2',
      1 => '',
      2 => 'default',
      3 => NULL,
      4 => 'text',
    ),
    'views' => 
    array (
      0 => 'views',
      1 => '',
      2 => 'default',
      3 => NULL,
      4 => 'hidden',
    ),
    '_content' => '<!DOCTYPE html>
<html> 
<head>  
    <base href="[[!++site_url]]">
    <title>Ошибка 404 | [[+set.site-name:default=`VEEP`]]</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8 " />
    <meta name="description" content=""/>
    <meta name="keywords" content=""/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="format-detection" content="telephone=no">
    <meta name="viewport" content="width=1200">
    
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/fonts/raleway.css" />
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/css/style.css" />
    <link rel="stylesheet" href="/assets/templates/default/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="/assets/extra/slick/slick.css" />
    <link rel="icon" href="/favicon.png">
    <!--[if IE]><link rel="shortcut icon" href="/favicon.ico"><![endif]-->
    <script type="text/javascript" src="/assets/extra/jquery.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/jquery.nav.js"></script>
<script src="https://cdn.jsdelivr.net/jquery.goodshare.js/3.2.3/goodshare.min.js"></script>

<script type="text/javascript" src="/assets/extra/slick/slick.min.js"></script>
<script src="/assets/extra/magnific-popup/jquery.magnific-popup.min.js"></script>
<link rel="stylesheet" href="/assets/extra/magnific-popup/magnific-popup.css">

<script type="text/javascript">
$(document).ready(function(){
    $(\'.zoom\').magnificPopup({
    type:\'image\'
});
$(\'.zoom-gal\').magnificPopup({
    type:\'image\',
    gallery: {enabled: true}
});
$(\'.modal-window\').magnificPopup({
    type:\'inline\'
});
$.extend(true, $.magnificPopup.defaults, { // перевод для magnific-popup
    tClose: \'Закрыть (Esc)\', // Alt text on close button
    tLoading: \'Загрузка...\', // Text that is displayed during loading. Can contain %curr% and %total% keys
    gallery: {
        tPrev: \'Предыдущий\', // Alt text on left arrow
        tNext: \'Следующий\', // Alt text on right arrow
        tCounter: \'%curr% из %total%\' // Markup for "1 of 7" counter
    },
    image: {
        tError: \'Не удалось загрузить <a href="%url%">изображение</a>.\' // Error message when image could not be loaded
    },
    ajax: {
        tError: \'Не удалось загрузить <a href="%url%">содержимое</a>.\' // Error message when ajax request failed
    }
});

    
    var sliderWrapper = $(\'.slider-wrapper\');
    sliderWrapper.removeClass(\'loading\');
    if (sliderWrapper.data(\'adaptive\') == \'1\') {
        $(window).resize(function() {
            var sliderHeight = parseInt(sliderWrapper.data(\'height\')) * $(sliderWrapper).width() / parseInt(sliderWrapper.data(\'width\'));
            sliderWrapper.css(\'height\', sliderHeight + \'px\');
        }).trigger(\'resize\');
    }
    
    $(\'.slick-slider\').show().slick({
        autoplay: true,
        speed: 800,
        autoplaySpeed: 5000,
        fade: true,
        arrows: true,
        dots: false,
        adaptiveHeight: true,
        prevArrow: \'<button type="button" class="slick-prev"></button>\',
        nextArrow: \'<button type="button" class="slick-next"></button>\'
    });
    
    $(\'.carousel-partners\').slick({
        infinite: true,
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: true,
        speed: 800,
        autoplaySpeed: 3000,
        prevArrow: \'<button type="button" class="slick-prev"><i class="fa fa-angle-left"></i></button>\',
        nextArrow: \'<button type="button" class="slick-next"><i class="fa fa-angle-right"></i></button>\'
    });
    
    $(\'#menuToggle, .menu-close\').on(\'click\', function(){
		$(\'#menuToggle\').toggleClass(\'active\');
		$(\'body\').toggleClass(\'body-push-toleft\');
		$(\'#theMenu\').toggleClass(\'menu-open\');
	});
	
	$(\'.full-wrapper\').click(function(){
	    $(\'#theMenu\').removeClass(\'menu-open\');
	});
    
    
    $(\'.menu\').onePageNav({
        currentClass: \'active\',
        changeHash: false,
        scrollSpeed: 1000,
        scrollThreshold: 0.5,
        filter: \'\',
        easing: \'swing\'
    });
    
    
    var headerFix = $(\'.header-bottom\');
    var headerTopHeight = $(\'.header-top\').height();
    $(window).scroll(function(){
		if ( $(this).scrollTop() > headerTopHeight && headerFix.hasClass(\'not-fixed\') ){
		    headerFix.removeClass(\'not-fixed\').addClass(\'header-fixed\');
		} else if($(this).scrollTop() <= headerTopHeight && headerFix.hasClass(\'header-fixed\')) {
			headerFix.removeClass(\'header-fixed\').addClass(\'not-fixed\');
		}
    });
});
</script>
    [[!getSiteSettings]]
</head>
<body id="top" [[+set.copy-protect:is=`1`:then=`oncontextmenu="return false;" onselectstart="return false;" style="-moz-user-select: none;user-select: none;"`]]>
    [[+set.button-to-top:is=`1`:then=`<script>
    $(function() {
        var toTopButton = $(\'#button-to-top\');
        var edgeValue = 100;
        var wind = $(window);

        $(window).scroll(function(e) {
            toTopButton.toggle(wind.scrollTop() > edgeValue);
        }).trigger(\'scroll\');
        toTopButton.click(function() {
            $(\'html, body\').animate({scrollTop: 0}, 300);
        });
    });
</script>

<div id="button-to-top" class="btn-top-top"><i class="fa fa-chevron-up"></i></div>`]]
    <nav class="menu" id="theMenu">
		<ul class="menu-wrap">
                <li class="logo-menu"><a href="#top">[[+set.site-name]]</a></li>
    			<i class="fa fa-close icon-remove menu-close"></i>
                <li><a href="#blok-3"  class="smoothScroll">Уже доступно в ресторанах</a></li><li><a href="#blok-1"  class="smoothScroll">Для чего это нужно</a></li><li><a href="#blok-2"  class="smoothScroll">Как это работает</a></li><li><a href="#kontaktyi"  class="smoothScroll">Контакты</a></li>
            </ul>
		<div id="menuToggle"><i class="fa fa-bars"></i></div>
	</nav>
    <div class="full-wrapper">
        <header class="header-main" [[!+set.bg-header:notempty=`style="background-image: url(/userfiles/[[!+set.bg-header]]);"`]]>
            <div class="header-top">
                <div class="container">
                    <div class="logo" role="banner">
                        [[!+set.logo:notempty=`<img src="/userfiles/[[+set.logo]]" alt="[[+set.site-name]]">`:empty=``]]
                    </div>
                    [[!+set.site-slogan:notempty=`<div class="slogan">[[+set.site-slogan]]</div>`]]
                    <div class="download-links">
                        <a href="https://play.google.com/store/apps/details?id=ru.veeppay.app" target="_blank"><img src="/userfiles/images/get-it-on-google-play.svg" alt="Google play" width="200"></a>
                        <a href="https://itunes.apple.com/ru/app/veep/id1109320099?mt=8" target="_blank"><img src="/userfiles/images/logoapp.svg" alt="App store" width="200"></a>
                    </div>
                </div>
            </div>
        </header>
        
        <main class="content">
<!-- контент страницы --><p>Страница не найдена. Возможно сменился её адрес. Попробуйте начать с <a href="/">главной страницы.</a></p>
        </main>
        <div class="pushfooter"></div>
    </div>
    <footer class="footer-main">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <span class="copyright">&copy; [[+set.site-name:default=`VEEP`]] - 2017</span>
                </div>
                <div class="col-md-4">
                    <div class="social-block">
    [[!+set.social-buttons:replace=`||==`]]
    [[!+set.msg-buttons:replace=`||==`]]
</div>
                </div>
                <div class="col-md-4">
                    <div class="footer-links">
                        <!--LiveInternet counter--><script type="text/javascript"><!--
document.write("<a href=\'//www.liveinternet.ru/click\' "+
"target=_blank><img src=\'//counter.yadro.ru/hit?t44.6;r"+
escape(document.referrer)+((typeof(screen)=="undefined")?"":
";s"+screen.width+"*"+screen.height+"*"+(screen.colorDepth?
screen.colorDepth:screen.pixelDepth))+";u"+escape(document.URL)+
";"+Math.random()+
"\' alt=\'\' title=\'LiveInternet\' "+
"border=\'0\' width=\'31\' height=\'31\'><\\/a>")
//--></script><!--/LiveInternet-->
                    </div>
                </div>
            </div
        </div>
    </footer>
    <div id="order-form" class="mfp-hide modal-block">
    <div class="modal-block-name">Заказать звонок</div>
    [[!AjaxForm@ajaxForm?
    &snippet=`FormIt`
    &form=`call-back-form`
    &hooks=`spam,email`
    &emailTpl=`call-back-email-report` 
    &emailTo=`[[+set.recipient-email:default=`webmaster@veeptest.ru`]]` 
    &emailSubject=`Сообщение с сайта VEEP`
    &emailFrom=`webmaster@veeptest.ru`
    &validate=`name:required, phone:required`
    &validationErrorMessage=`В форме содержатся ошибки!`
    &successMessage=`Сообщение успешно отправлено`
]]
</div>

<div id="order-form2" class="mfp-hide modal-block">
    <div class="modal-block-name">Перезвоните мне прямо сейчас</div>
    [[!AjaxForm@ajaxForm?
    &snippet=`FormIt`
    &form=`call-back-form`
    &hooks=`spam,email`
    &emailTpl=`call-back-email-report` 
    &emailTo=`[[+set.recipient-email:default=`webmaster@veeptest.ru`]]` 
    &emailSubject=`Сообщение с сайта VEEP`
    &emailFrom=`webmaster@veeptest.ru`
    &validate=`name:required, phone:required`
    &validationErrorMessage=`В форме содержатся ошибки!`
    &successMessage=`Сообщение успешно отправлено`
]]
</div>
    [[+set.ya-counter-id:notempty=`
    <!-- Yandex.Metrika counter -->
    <script type="text/javascript">
        (function (d, w, c) {
            (w[c] = w[c] || []).push(function() {
                try {
                    w.yaCounter[[+set.ya-counter-id]] = new Ya.Metrika({
                        id:[[+set.ya-counter-id]],
                        clickmap:true,
                        trackLinks:true,
                        accurateTrackBounce:true,
                        webvisor:true
                    });
                } catch(e) { }
            });
    
            var n = d.getElementsByTagName("script")[0],
                s = d.createElement("script"),
                f = function () { n.parentNode.insertBefore(s, n); };
            s.type = "text/javascript";
            s.async = true;
            s.src = "https://mc.yandex.ru/metrika/watch.js";
    
            if (w.opera == "[object Opera]") {
                d.addEventListener("DOMContentLoaded", f, false);
            } else { f(); }
        })(document, window, "yandex_metrika_callbacks");
    </script>
    <noscript><div><img src="https://mc.yandex.ru/watch/[[+set.ya-counter-id]]" style="position:absolute; left:-9999px;" alt="" /></div></noscript>
    <!-- /Yandex.Metrika counter -->
`]]

    [[+set.ga-id:notempty=`
    <script>
      (function(i,s,o,g,r,a,m){i[\'GoogleAnalyticsObject\']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,\'script\',\'//www.google-analytics.com/analytics.js\',\'ga\');
    
      ga(\'create\', \'[[+set.ga-id]]\', \'auto\');
      ga(\'send\', \'pageview\');
    
    </script>
`]]
    
</body>
</html>',
    '_isForward' => true,
  ),
  'contentType' => 
  array (
    'id' => 1,
    'name' => 'HTML',
    'description' => 'HTML content',
    'mime_type' => 'text/html',
    'file_extensions' => '.html',
    'headers' => '',
    'binary' => 0,
  ),
  'policyCache' => 
  array (
  ),
  'elementCache' => 
  array (
    '[[$scripts-system]]' => '$(\'.zoom\').magnificPopup({
    type:\'image\'
});
$(\'.zoom-gal\').magnificPopup({
    type:\'image\',
    gallery: {enabled: true}
});
$(\'.modal-window\').magnificPopup({
    type:\'inline\'
});
$.extend(true, $.magnificPopup.defaults, { // перевод для magnific-popup
    tClose: \'Закрыть (Esc)\', // Alt text on close button
    tLoading: \'Загрузка...\', // Text that is displayed during loading. Can contain %curr% and %total% keys
    gallery: {
        tPrev: \'Предыдущий\', // Alt text on left arrow
        tNext: \'Следующий\', // Alt text on right arrow
        tCounter: \'%curr% из %total%\' // Markup for "1 of 7" counter
    },
    image: {
        tError: \'Не удалось загрузить <a href="%url%">изображение</a>.\' // Error message when image could not be loaded
    },
    ajax: {
        tError: \'Не удалось загрузить <a href="%url%">содержимое</a>.\' // Error message when ajax request failed
    }
});
',
    '[[$scripts]]' => '<script type="text/javascript" src="/assets/extra/jquery.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/jquery.nav.js"></script>
<script src="https://cdn.jsdelivr.net/jquery.goodshare.js/3.2.3/goodshare.min.js"></script>

<script type="text/javascript" src="/assets/extra/slick/slick.min.js"></script>
<script src="/assets/extra/magnific-popup/jquery.magnific-popup.min.js"></script>
<link rel="stylesheet" href="/assets/extra/magnific-popup/magnific-popup.css">

<script type="text/javascript">
$(document).ready(function(){
    $(\'.zoom\').magnificPopup({
    type:\'image\'
});
$(\'.zoom-gal\').magnificPopup({
    type:\'image\',
    gallery: {enabled: true}
});
$(\'.modal-window\').magnificPopup({
    type:\'inline\'
});
$.extend(true, $.magnificPopup.defaults, { // перевод для magnific-popup
    tClose: \'Закрыть (Esc)\', // Alt text on close button
    tLoading: \'Загрузка...\', // Text that is displayed during loading. Can contain %curr% and %total% keys
    gallery: {
        tPrev: \'Предыдущий\', // Alt text on left arrow
        tNext: \'Следующий\', // Alt text on right arrow
        tCounter: \'%curr% из %total%\' // Markup for "1 of 7" counter
    },
    image: {
        tError: \'Не удалось загрузить <a href="%url%">изображение</a>.\' // Error message when image could not be loaded
    },
    ajax: {
        tError: \'Не удалось загрузить <a href="%url%">содержимое</a>.\' // Error message when ajax request failed
    }
});

    
    var sliderWrapper = $(\'.slider-wrapper\');
    sliderWrapper.removeClass(\'loading\');
    if (sliderWrapper.data(\'adaptive\') == \'1\') {
        $(window).resize(function() {
            var sliderHeight = parseInt(sliderWrapper.data(\'height\')) * $(sliderWrapper).width() / parseInt(sliderWrapper.data(\'width\'));
            sliderWrapper.css(\'height\', sliderHeight + \'px\');
        }).trigger(\'resize\');
    }
    
    $(\'.slick-slider\').show().slick({
        autoplay: true,
        speed: 800,
        autoplaySpeed: 5000,
        fade: true,
        arrows: true,
        dots: false,
        adaptiveHeight: true,
        prevArrow: \'<button type="button" class="slick-prev"></button>\',
        nextArrow: \'<button type="button" class="slick-next"></button>\'
    });
    
    $(\'.carousel-partners\').slick({
        infinite: true,
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: true,
        speed: 800,
        autoplaySpeed: 3000,
        prevArrow: \'<button type="button" class="slick-prev"><i class="fa fa-angle-left"></i></button>\',
        nextArrow: \'<button type="button" class="slick-next"><i class="fa fa-angle-right"></i></button>\'
    });
    
    $(\'#menuToggle, .menu-close\').on(\'click\', function(){
		$(\'#menuToggle\').toggleClass(\'active\');
		$(\'body\').toggleClass(\'body-push-toleft\');
		$(\'#theMenu\').toggleClass(\'menu-open\');
	});
	
	$(\'.full-wrapper\').click(function(){
	    $(\'#theMenu\').removeClass(\'menu-open\');
	});
    
    
    $(\'.menu\').onePageNav({
        currentClass: \'active\',
        changeHash: false,
        scrollSpeed: 1000,
        scrollThreshold: 0.5,
        filter: \'\',
        easing: \'swing\'
    });
    
    
    var headerFix = $(\'.header-bottom\');
    var headerTopHeight = $(\'.header-top\').height();
    $(window).scroll(function(){
		if ( $(this).scrollTop() > headerTopHeight && headerFix.hasClass(\'not-fixed\') ){
		    headerFix.removeClass(\'not-fixed\').addClass(\'header-fixed\');
		} else if($(this).scrollTop() <= headerTopHeight && headerFix.hasClass(\'header-fixed\')) {
			headerFix.removeClass(\'header-fixed\').addClass(\'not-fixed\');
		}
    });
});
</script>',
    '[[$button-to-top]]' => '<script>
    $(function() {
        var toTopButton = $(\'#button-to-top\');
        var edgeValue = 100;
        var wind = $(window);

        $(window).scroll(function(e) {
            toTopButton.toggle(wind.scrollTop() > edgeValue);
        }).trigger(\'scroll\');
        toTopButton.click(function() {
            $(\'html, body\').animate({scrollTop: 0}, 300);
        });
    });
</script>

<div id="button-to-top" class="btn-top-top"><i class="fa fa-chevron-up"></i></div>',
    '[[pdoMenu?
            &parents=`0`
            &level=`1`
            &outerClass=`menu-wrap`
            &tplOuter=`@INLINE
            <ul[[+classes]]>
                <li class="logo-menu"><a href="#top">[[+set.site-name]]</a></li>
    			<i class="fa fa-close icon-remove menu-close"></i>
                [[+wrapper]]
            </ul>
            `
            &rowClass=`smoothScroll`
            &tpl=`@INLINE <li><a href="#[[+alias]]" [[+attributes]] class="smoothScroll">[[+menutitle]]</a>[[+wrapper]]</li>`
        ]]' => '<ul class="menu-wrap">
                <li class="logo-menu"><a href="#top">[[+set.site-name]]</a></li>
    			<i class="fa fa-close icon-remove menu-close"></i>
                <li><a href="#blok-3"  class="smoothScroll">Уже доступно в ресторанах</a></li><li><a href="#blok-1"  class="smoothScroll">Для чего это нужно</a></li><li><a href="#blok-2"  class="smoothScroll">Как это работает</a></li><li><a href="#kontaktyi"  class="smoothScroll">Контакты</a></li>
            </ul>',
    '[[$header]]' => '<!DOCTYPE html>
<html> 
<head>  
    <base href="[[!++site_url]]">
    <title>Ошибка 404 | [[+set.site-name:default=`VEEP`]]</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8 " />
    <meta name="description" content=""/>
    <meta name="keywords" content=""/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="format-detection" content="telephone=no">
    <meta name="viewport" content="width=1200">
    
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/fonts/raleway.css" />
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/css/style.css" />
    <link rel="stylesheet" href="/assets/templates/default/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="/assets/extra/slick/slick.css" />
    <link rel="icon" href="/favicon.png">
    <!--[if IE]><link rel="shortcut icon" href="/favicon.ico"><![endif]-->
    <script type="text/javascript" src="/assets/extra/jquery.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/jquery.nav.js"></script>
<script src="https://cdn.jsdelivr.net/jquery.goodshare.js/3.2.3/goodshare.min.js"></script>

<script type="text/javascript" src="/assets/extra/slick/slick.min.js"></script>
<script src="/assets/extra/magnific-popup/jquery.magnific-popup.min.js"></script>
<link rel="stylesheet" href="/assets/extra/magnific-popup/magnific-popup.css">

<script type="text/javascript">
$(document).ready(function(){
    $(\'.zoom\').magnificPopup({
    type:\'image\'
});
$(\'.zoom-gal\').magnificPopup({
    type:\'image\',
    gallery: {enabled: true}
});
$(\'.modal-window\').magnificPopup({
    type:\'inline\'
});
$.extend(true, $.magnificPopup.defaults, { // перевод для magnific-popup
    tClose: \'Закрыть (Esc)\', // Alt text on close button
    tLoading: \'Загрузка...\', // Text that is displayed during loading. Can contain %curr% and %total% keys
    gallery: {
        tPrev: \'Предыдущий\', // Alt text on left arrow
        tNext: \'Следующий\', // Alt text on right arrow
        tCounter: \'%curr% из %total%\' // Markup for "1 of 7" counter
    },
    image: {
        tError: \'Не удалось загрузить <a href="%url%">изображение</a>.\' // Error message when image could not be loaded
    },
    ajax: {
        tError: \'Не удалось загрузить <a href="%url%">содержимое</a>.\' // Error message when ajax request failed
    }
});

    
    var sliderWrapper = $(\'.slider-wrapper\');
    sliderWrapper.removeClass(\'loading\');
    if (sliderWrapper.data(\'adaptive\') == \'1\') {
        $(window).resize(function() {
            var sliderHeight = parseInt(sliderWrapper.data(\'height\')) * $(sliderWrapper).width() / parseInt(sliderWrapper.data(\'width\'));
            sliderWrapper.css(\'height\', sliderHeight + \'px\');
        }).trigger(\'resize\');
    }
    
    $(\'.slick-slider\').show().slick({
        autoplay: true,
        speed: 800,
        autoplaySpeed: 5000,
        fade: true,
        arrows: true,
        dots: false,
        adaptiveHeight: true,
        prevArrow: \'<button type="button" class="slick-prev"></button>\',
        nextArrow: \'<button type="button" class="slick-next"></button>\'
    });
    
    $(\'.carousel-partners\').slick({
        infinite: true,
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: true,
        speed: 800,
        autoplaySpeed: 3000,
        prevArrow: \'<button type="button" class="slick-prev"><i class="fa fa-angle-left"></i></button>\',
        nextArrow: \'<button type="button" class="slick-next"><i class="fa fa-angle-right"></i></button>\'
    });
    
    $(\'#menuToggle, .menu-close\').on(\'click\', function(){
		$(\'#menuToggle\').toggleClass(\'active\');
		$(\'body\').toggleClass(\'body-push-toleft\');
		$(\'#theMenu\').toggleClass(\'menu-open\');
	});
	
	$(\'.full-wrapper\').click(function(){
	    $(\'#theMenu\').removeClass(\'menu-open\');
	});
    
    
    $(\'.menu\').onePageNav({
        currentClass: \'active\',
        changeHash: false,
        scrollSpeed: 1000,
        scrollThreshold: 0.5,
        filter: \'\',
        easing: \'swing\'
    });
    
    
    var headerFix = $(\'.header-bottom\');
    var headerTopHeight = $(\'.header-top\').height();
    $(window).scroll(function(){
		if ( $(this).scrollTop() > headerTopHeight && headerFix.hasClass(\'not-fixed\') ){
		    headerFix.removeClass(\'not-fixed\').addClass(\'header-fixed\');
		} else if($(this).scrollTop() <= headerTopHeight && headerFix.hasClass(\'header-fixed\')) {
			headerFix.removeClass(\'header-fixed\').addClass(\'not-fixed\');
		}
    });
});
</script>
    [[!getSiteSettings]]
</head>
<body id="top" [[+set.copy-protect:is=`1`:then=`oncontextmenu="return false;" onselectstart="return false;" style="-moz-user-select: none;user-select: none;"`]]>
    [[+set.button-to-top:is=`1`:then=`<script>
    $(function() {
        var toTopButton = $(\'#button-to-top\');
        var edgeValue = 100;
        var wind = $(window);

        $(window).scroll(function(e) {
            toTopButton.toggle(wind.scrollTop() > edgeValue);
        }).trigger(\'scroll\');
        toTopButton.click(function() {
            $(\'html, body\').animate({scrollTop: 0}, 300);
        });
    });
</script>

<div id="button-to-top" class="btn-top-top"><i class="fa fa-chevron-up"></i></div>`]]
    <nav class="menu" id="theMenu">
		<ul class="menu-wrap">
                <li class="logo-menu"><a href="#top">[[+set.site-name]]</a></li>
    			<i class="fa fa-close icon-remove menu-close"></i>
                <li><a href="#blok-3"  class="smoothScroll">Уже доступно в ресторанах</a></li><li><a href="#blok-1"  class="smoothScroll">Для чего это нужно</a></li><li><a href="#blok-2"  class="smoothScroll">Как это работает</a></li><li><a href="#kontaktyi"  class="smoothScroll">Контакты</a></li>
            </ul>
		<div id="menuToggle"><i class="fa fa-bars"></i></div>
	</nav>
    <div class="full-wrapper">
        <header class="header-main" [[!+set.bg-header:notempty=`style="background-image: url(/userfiles/[[!+set.bg-header]]);"`]]>
            <div class="header-top">
                <div class="container">
                    <div class="logo" role="banner">
                        [[!+set.logo:notempty=`<img src="/userfiles/[[+set.logo]]" alt="[[+set.site-name]]">`:empty=``]]
                    </div>
                    [[!+set.site-slogan:notempty=`<div class="slogan">[[+set.site-slogan]]</div>`]]
                    <div class="download-links">
                        <a href="https://play.google.com/store/apps/details?id=ru.veeppay.app" target="_blank"><img src="/userfiles/images/get-it-on-google-play.svg" alt="Google play" width="200"></a>
                        <a href="https://itunes.apple.com/ru/app/veep/id1109320099?mt=8" target="_blank"><img src="/userfiles/images/logoapp.svg" alt="App store" width="200"></a>
                    </div>
                </div>
            </div>
        </header>
        
        <main class="content">',
    '[[currentYear]]' => '2017',
    '[[$share]]' => '<div class="social-block">
    [[!+set.social-buttons:replace=`||==`]]
    [[!+set.msg-buttons:replace=`||==`]]
</div>',
    '[[$lirucounter]]' => '<!--LiveInternet counter--><script type="text/javascript"><!--
document.write("<a href=\'//www.liveinternet.ru/click\' "+
"target=_blank><img src=\'//counter.yadro.ru/hit?t44.6;r"+
escape(document.referrer)+((typeof(screen)=="undefined")?"":
";s"+screen.width+"*"+screen.height+"*"+(screen.colorDepth?
screen.colorDepth:screen.pixelDepth))+";u"+escape(document.URL)+
";"+Math.random()+
"\' alt=\'\' title=\'LiveInternet\' "+
"border=\'0\' width=\'31\' height=\'31\'><\\/a>")
//--></script><!--/LiveInternet-->',
    '[[domain]]' => 'veeptest.ru',
    '[[$call-back-form-ajax]]' => '[[!AjaxForm@ajaxForm?
    &snippet=`FormIt`
    &form=`call-back-form`
    &hooks=`spam,email`
    &emailTpl=`call-back-email-report` 
    &emailTo=`[[+set.recipient-email:default=`webmaster@veeptest.ru`]]` 
    &emailSubject=`Сообщение с сайта VEEP`
    &emailFrom=`webmaster@veeptest.ru`
    &validate=`name:required, phone:required`
    &validationErrorMessage=`В форме содержатся ошибки!`
    &successMessage=`Сообщение успешно отправлено`
]]',
    '[[$forms]]' => '<div id="order-form" class="mfp-hide modal-block">
    <div class="modal-block-name">Заказать звонок</div>
    [[!AjaxForm@ajaxForm?
    &snippet=`FormIt`
    &form=`call-back-form`
    &hooks=`spam,email`
    &emailTpl=`call-back-email-report` 
    &emailTo=`[[+set.recipient-email:default=`webmaster@veeptest.ru`]]` 
    &emailSubject=`Сообщение с сайта VEEP`
    &emailFrom=`webmaster@veeptest.ru`
    &validate=`name:required, phone:required`
    &validationErrorMessage=`В форме содержатся ошибки!`
    &successMessage=`Сообщение успешно отправлено`
]]
</div>

<div id="order-form2" class="mfp-hide modal-block">
    <div class="modal-block-name">Перезвоните мне прямо сейчас</div>
    [[!AjaxForm@ajaxForm?
    &snippet=`FormIt`
    &form=`call-back-form`
    &hooks=`spam,email`
    &emailTpl=`call-back-email-report` 
    &emailTo=`[[+set.recipient-email:default=`webmaster@veeptest.ru`]]` 
    &emailSubject=`Сообщение с сайта VEEP`
    &emailFrom=`webmaster@veeptest.ru`
    &validate=`name:required, phone:required`
    &validationErrorMessage=`В форме содержатся ошибки!`
    &successMessage=`Сообщение успешно отправлено`
]]
</div>',
    '[[$ya-metrika]]' => '[[+set.ya-counter-id:notempty=`
    <!-- Yandex.Metrika counter -->
    <script type="text/javascript">
        (function (d, w, c) {
            (w[c] = w[c] || []).push(function() {
                try {
                    w.yaCounter[[+set.ya-counter-id]] = new Ya.Metrika({
                        id:[[+set.ya-counter-id]],
                        clickmap:true,
                        trackLinks:true,
                        accurateTrackBounce:true,
                        webvisor:true
                    });
                } catch(e) { }
            });
    
            var n = d.getElementsByTagName("script")[0],
                s = d.createElement("script"),
                f = function () { n.parentNode.insertBefore(s, n); };
            s.type = "text/javascript";
            s.async = true;
            s.src = "https://mc.yandex.ru/metrika/watch.js";
    
            if (w.opera == "[object Opera]") {
                d.addEventListener("DOMContentLoaded", f, false);
            } else { f(); }
        })(document, window, "yandex_metrika_callbacks");
    </script>
    <noscript><div><img src="https://mc.yandex.ru/watch/[[+set.ya-counter-id]]" style="position:absolute; left:-9999px;" alt="" /></div></noscript>
    <!-- /Yandex.Metrika counter -->
`]]
',
    '[[$google-analytics]]' => '[[+set.ga-id:notempty=`
    <script>
      (function(i,s,o,g,r,a,m){i[\'GoogleAnalyticsObject\']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,\'script\',\'//www.google-analytics.com/analytics.js\',\'ga\');
    
      ga(\'create\', \'[[+set.ga-id]]\', \'auto\');
      ga(\'send\', \'pageview\');
    
    </script>
`]]',
    '[[$footer]]' => '        </main>
        <div class="pushfooter"></div>
    </div>
    <footer class="footer-main">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <span class="copyright">&copy; [[+set.site-name:default=`VEEP`]] - 2017</span>
                </div>
                <div class="col-md-4">
                    <div class="social-block">
    [[!+set.social-buttons:replace=`||==`]]
    [[!+set.msg-buttons:replace=`||==`]]
</div>
                </div>
                <div class="col-md-4">
                    <div class="footer-links">
                        <!--LiveInternet counter--><script type="text/javascript"><!--
document.write("<a href=\'//www.liveinternet.ru/click\' "+
"target=_blank><img src=\'//counter.yadro.ru/hit?t44.6;r"+
escape(document.referrer)+((typeof(screen)=="undefined")?"":
";s"+screen.width+"*"+screen.height+"*"+(screen.colorDepth?
screen.colorDepth:screen.pixelDepth))+";u"+escape(document.URL)+
";"+Math.random()+
"\' alt=\'\' title=\'LiveInternet\' "+
"border=\'0\' width=\'31\' height=\'31\'><\\/a>")
//--></script><!--/LiveInternet-->
                    </div>
                </div>
            </div
        </div>
    </footer>
    <div id="order-form" class="mfp-hide modal-block">
    <div class="modal-block-name">Заказать звонок</div>
    [[!AjaxForm@ajaxForm?
    &snippet=`FormIt`
    &form=`call-back-form`
    &hooks=`spam,email`
    &emailTpl=`call-back-email-report` 
    &emailTo=`[[+set.recipient-email:default=`webmaster@veeptest.ru`]]` 
    &emailSubject=`Сообщение с сайта VEEP`
    &emailFrom=`webmaster@veeptest.ru`
    &validate=`name:required, phone:required`
    &validationErrorMessage=`В форме содержатся ошибки!`
    &successMessage=`Сообщение успешно отправлено`
]]
</div>

<div id="order-form2" class="mfp-hide modal-block">
    <div class="modal-block-name">Перезвоните мне прямо сейчас</div>
    [[!AjaxForm@ajaxForm?
    &snippet=`FormIt`
    &form=`call-back-form`
    &hooks=`spam,email`
    &emailTpl=`call-back-email-report` 
    &emailTo=`[[+set.recipient-email:default=`webmaster@veeptest.ru`]]` 
    &emailSubject=`Сообщение с сайта VEEP`
    &emailFrom=`webmaster@veeptest.ru`
    &validate=`name:required, phone:required`
    &validationErrorMessage=`В форме содержатся ошибки!`
    &successMessage=`Сообщение успешно отправлено`
]]
</div>
    [[+set.ya-counter-id:notempty=`
    <!-- Yandex.Metrika counter -->
    <script type="text/javascript">
        (function (d, w, c) {
            (w[c] = w[c] || []).push(function() {
                try {
                    w.yaCounter[[+set.ya-counter-id]] = new Ya.Metrika({
                        id:[[+set.ya-counter-id]],
                        clickmap:true,
                        trackLinks:true,
                        accurateTrackBounce:true,
                        webvisor:true
                    });
                } catch(e) { }
            });
    
            var n = d.getElementsByTagName("script")[0],
                s = d.createElement("script"),
                f = function () { n.parentNode.insertBefore(s, n); };
            s.type = "text/javascript";
            s.async = true;
            s.src = "https://mc.yandex.ru/metrika/watch.js";
    
            if (w.opera == "[object Opera]") {
                d.addEventListener("DOMContentLoaded", f, false);
            } else { f(); }
        })(document, window, "yandex_metrika_callbacks");
    </script>
    <noscript><div><img src="https://mc.yandex.ru/watch/[[+set.ya-counter-id]]" style="position:absolute; left:-9999px;" alt="" /></div></noscript>
    <!-- /Yandex.Metrika counter -->
`]]

    [[+set.ga-id:notempty=`
    <script>
      (function(i,s,o,g,r,a,m){i[\'GoogleAnalyticsObject\']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,\'script\',\'//www.google-analytics.com/analytics.js\',\'ga\');
    
      ga(\'create\', \'[[+set.ga-id]]\', \'auto\');
      ga(\'send\', \'pageview\');
    
    </script>
`]]
    
</body>
</html>',
  ),
  'sourceCache' => 
  array (
    'modChunk' => 
    array (
      'header' => 
      array (
        'fields' => 
        array (
          'id' => 1,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'header',
          'description' => '',
          'editor_type' => 0,
          'category' => 12,
          'cache_type' => 0,
          'snippet' => '<!DOCTYPE html>
<html> 
<head>  
    <base href="[[!++site_url]]">
    <title>[[*meta_title:default=`[[*longtitle:default=`[[*pagetitle]]`]]`]] | [[+set.site-name:default=`[[++site_name]]`]]</title>
    <meta http-equiv="Content-Type" content="text/html; charset=[[++modx_charset]] " />
    <meta name="description" content="[[*description]]"/>
    <meta name="keywords" content="[[*keywords]]"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="format-detection" content="telephone=no">
    <meta name="viewport" content="width=1200">
    
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/fonts/raleway.css" />
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/css/style.css" />
    <link rel="stylesheet" href="/assets/templates/default/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="/assets/extra/slick/slick.css" />
    <link rel="icon" href="/favicon.png">
    <!--[if IE]><link rel="shortcut icon" href="/favicon.ico"><![endif]-->
    [[$scripts]]
    [[!getSiteSettings]]
</head>
<body id="top" [[+set.copy-protect:is=`1`:then=`oncontextmenu="return false;" onselectstart="return false;" style="-moz-user-select: none;user-select: none;"`]]>
    [[+set.button-to-top:is=`1`:then=`[[$button-to-top]]`]]
    <nav class="menu" id="theMenu">
		[[pdoMenu?
            &parents=`0`
            &level=`1`
            &outerClass=`menu-wrap`
            &tplOuter=`@INLINE
            <ul[[+classes]]>
                <li class="logo-menu"><a href="#top">[[+set.site-name]]</a></li>
    			<i class="fa fa-close icon-remove menu-close"></i>
                [[+wrapper]]
            </ul>
            `
            &rowClass=`smoothScroll`
            &tpl=`@INLINE <li><a href="#[[+alias]]" [[+attributes]] class="smoothScroll">[[+menutitle]]</a>[[+wrapper]]</li>`
        ]]
		<div id="menuToggle"><i class="fa fa-bars"></i></div>
	</nav>
    <div class="full-wrapper">
        <header class="header-main" [[!+set.bg-header:notempty=`style="background-image: url(/userfiles/[[!+set.bg-header]]);"`]]>
            <div class="header-top">
                <div class="container">
                    <div class="logo" role="banner">
                        [[!+set.logo:notempty=`<img src="/userfiles/[[+set.logo]]" alt="[[+set.site-name]]">`:empty=``]]
                    </div>
                    [[!+set.site-slogan:notempty=`<div class="slogan">[[+set.site-slogan]]</div>`]]
                    <div class="download-links">
                        <a href="https://play.google.com/store/apps/details?id=ru.veeppay.app" target="_blank"><img src="/userfiles/images/get-it-on-google-play.svg" alt="Google play" width="200"></a>
                        <a href="https://itunes.apple.com/ru/app/veep/id1109320099?mt=8" target="_blank"><img src="/userfiles/images/logoapp.svg" alt="App store" width="200"></a>
                    </div>
                </div>
            </div>
        </header>
        [[-!smarty?
            &tpl=`slider`
            &slider_show=`[[+set.slider-show]]`
            &slideWidth=`1920`
            &slideHeight=`500`
            &adaptive=`1`
        ]]
        <main class="content">',
          'locked' => false,
          'properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '<!DOCTYPE html>
<html> 
<head>  
    <base href="[[!++site_url]]">
    <title>[[*meta_title:default=`[[*longtitle:default=`[[*pagetitle]]`]]`]] | [[+set.site-name:default=`[[++site_name]]`]]</title>
    <meta http-equiv="Content-Type" content="text/html; charset=[[++modx_charset]] " />
    <meta name="description" content="[[*description]]"/>
    <meta name="keywords" content="[[*keywords]]"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="format-detection" content="telephone=no">
    <meta name="viewport" content="width=1200">
    
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/fonts/raleway.css" />
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/templates/default/css/style.css" />
    <link rel="stylesheet" href="/assets/templates/default/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="/assets/extra/slick/slick.css" />
    <link rel="icon" href="/favicon.png">
    <!--[if IE]><link rel="shortcut icon" href="/favicon.ico"><![endif]-->
    [[$scripts]]
    [[!getSiteSettings]]
</head>
<body id="top" [[+set.copy-protect:is=`1`:then=`oncontextmenu="return false;" onselectstart="return false;" style="-moz-user-select: none;user-select: none;"`]]>
    [[+set.button-to-top:is=`1`:then=`[[$button-to-top]]`]]
    <nav class="menu" id="theMenu">
		[[pdoMenu?
            &parents=`0`
            &level=`1`
            &outerClass=`menu-wrap`
            &tplOuter=`@INLINE
            <ul[[+classes]]>
                <li class="logo-menu"><a href="#top">[[+set.site-name]]</a></li>
    			<i class="fa fa-close icon-remove menu-close"></i>
                [[+wrapper]]
            </ul>
            `
            &rowClass=`smoothScroll`
            &tpl=`@INLINE <li><a href="#[[+alias]]" [[+attributes]] class="smoothScroll">[[+menutitle]]</a>[[+wrapper]]</li>`
        ]]
		<div id="menuToggle"><i class="fa fa-bars"></i></div>
	</nav>
    <div class="full-wrapper">
        <header class="header-main" [[!+set.bg-header:notempty=`style="background-image: url(/userfiles/[[!+set.bg-header]]);"`]]>
            <div class="header-top">
                <div class="container">
                    <div class="logo" role="banner">
                        [[!+set.logo:notempty=`<img src="/userfiles/[[+set.logo]]" alt="[[+set.site-name]]">`:empty=``]]
                    </div>
                    [[!+set.site-slogan:notempty=`<div class="slogan">[[+set.site-slogan]]</div>`]]
                    <div class="download-links">
                        <a href="https://play.google.com/store/apps/details?id=ru.veeppay.app" target="_blank"><img src="/userfiles/images/get-it-on-google-play.svg" alt="Google play" width="200"></a>
                        <a href="https://itunes.apple.com/ru/app/veep/id1109320099?mt=8" target="_blank"><img src="/userfiles/images/logoapp.svg" alt="App store" width="200"></a>
                    </div>
                </div>
            </div>
        </header>
        [[-!smarty?
            &tpl=`slider`
            &slider_show=`[[+set.slider-show]]`
            &slideWidth=`1920`
            &slideHeight=`500`
            &adaptive=`1`
        ]]
        <main class="content">',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'scripts' => 
      array (
        'fields' => 
        array (
          'id' => 7,
          'source' => 0,
          'property_preprocess' => false,
          'name' => 'scripts',
          'description' => '',
          'editor_type' => 0,
          'category' => 12,
          'cache_type' => 0,
          'snippet' => '<script type="text/javascript" src="/assets/extra/jquery.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/jquery.nav.js"></script>
<script src="https://cdn.jsdelivr.net/jquery.goodshare.js/3.2.3/goodshare.min.js"></script>

<script type="text/javascript" src="/assets/extra/slick/slick.min.js"></script>
<script src="/assets/extra/magnific-popup/jquery.magnific-popup.min.js"></script>
<link rel="stylesheet" href="/assets/extra/magnific-popup/magnific-popup.css">

<script type="text/javascript">
$(document).ready(function(){
    [[$scripts-system]]
    
    var sliderWrapper = $(\'.slider-wrapper\');
    sliderWrapper.removeClass(\'loading\');
    if (sliderWrapper.data(\'adaptive\') == \'1\') {
        $(window).resize(function() {
            var sliderHeight = parseInt(sliderWrapper.data(\'height\')) * $(sliderWrapper).width() / parseInt(sliderWrapper.data(\'width\'));
            sliderWrapper.css(\'height\', sliderHeight + \'px\');
        }).trigger(\'resize\');
    }
    
    $(\'.slick-slider\').show().slick({
        autoplay: true,
        speed: 800,
        autoplaySpeed: 5000,
        fade: true,
        arrows: true,
        dots: false,
        adaptiveHeight: true,
        prevArrow: \'<button type="button" class="slick-prev"></button>\',
        nextArrow: \'<button type="button" class="slick-next"></button>\'
    });
    
    $(\'.carousel-partners\').slick({
        infinite: true,
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: true,
        speed: 800,
        autoplaySpeed: 3000,
        prevArrow: \'<button type="button" class="slick-prev"><i class="fa fa-angle-left"></i></button>\',
        nextArrow: \'<button type="button" class="slick-next"><i class="fa fa-angle-right"></i></button>\'
    });
    
    $(\'#menuToggle, .menu-close\').on(\'click\', function(){
		$(\'#menuToggle\').toggleClass(\'active\');
		$(\'body\').toggleClass(\'body-push-toleft\');
		$(\'#theMenu\').toggleClass(\'menu-open\');
	});
	
	$(\'.full-wrapper\').click(function(){
	    $(\'#theMenu\').removeClass(\'menu-open\');
	});
    
    
    $(\'.menu\').onePageNav({
        currentClass: \'active\',
        changeHash: false,
        scrollSpeed: 1000,
        scrollThreshold: 0.5,
        filter: \'\',
        easing: \'swing\'
    });
    
    
    var headerFix = $(\'.header-bottom\');
    var headerTopHeight = $(\'.header-top\').height();
    $(window).scroll(function(){
		if ( $(this).scrollTop() > headerTopHeight && headerFix.hasClass(\'not-fixed\') ){
		    headerFix.removeClass(\'not-fixed\').addClass(\'header-fixed\');
		} else if($(this).scrollTop() <= headerTopHeight && headerFix.hasClass(\'header-fixed\')) {
			headerFix.removeClass(\'header-fixed\').addClass(\'not-fixed\');
		}
    });
});
</script>',
          'locked' => false,
          'properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '<script type="text/javascript" src="/assets/extra/jquery.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/assets/templates/default/js/jquery.nav.js"></script>
<script src="https://cdn.jsdelivr.net/jquery.goodshare.js/3.2.3/goodshare.min.js"></script>

<script type="text/javascript" src="/assets/extra/slick/slick.min.js"></script>
<script src="/assets/extra/magnific-popup/jquery.magnific-popup.min.js"></script>
<link rel="stylesheet" href="/assets/extra/magnific-popup/magnific-popup.css">

<script type="text/javascript">
$(document).ready(function(){
    [[$scripts-system]]
    
    var sliderWrapper = $(\'.slider-wrapper\');
    sliderWrapper.removeClass(\'loading\');
    if (sliderWrapper.data(\'adaptive\') == \'1\') {
        $(window).resize(function() {
            var sliderHeight = parseInt(sliderWrapper.data(\'height\')) * $(sliderWrapper).width() / parseInt(sliderWrapper.data(\'width\'));
            sliderWrapper.css(\'height\', sliderHeight + \'px\');
        }).trigger(\'resize\');
    }
    
    $(\'.slick-slider\').show().slick({
        autoplay: true,
        speed: 800,
        autoplaySpeed: 5000,
        fade: true,
        arrows: true,
        dots: false,
        adaptiveHeight: true,
        prevArrow: \'<button type="button" class="slick-prev"></button>\',
        nextArrow: \'<button type="button" class="slick-next"></button>\'
    });
    
    $(\'.carousel-partners\').slick({
        infinite: true,
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: true,
        speed: 800,
        autoplaySpeed: 3000,
        prevArrow: \'<button type="button" class="slick-prev"><i class="fa fa-angle-left"></i></button>\',
        nextArrow: \'<button type="button" class="slick-next"><i class="fa fa-angle-right"></i></button>\'
    });
    
    $(\'#menuToggle, .menu-close\').on(\'click\', function(){
		$(\'#menuToggle\').toggleClass(\'active\');
		$(\'body\').toggleClass(\'body-push-toleft\');
		$(\'#theMenu\').toggleClass(\'menu-open\');
	});
	
	$(\'.full-wrapper\').click(function(){
	    $(\'#theMenu\').removeClass(\'menu-open\');
	});
    
    
    $(\'.menu\').onePageNav({
        currentClass: \'active\',
        changeHash: false,
        scrollSpeed: 1000,
        scrollThreshold: 0.5,
        filter: \'\',
        easing: \'swing\'
    });
    
    
    var headerFix = $(\'.header-bottom\');
    var headerTopHeight = $(\'.header-top\').height();
    $(window).scroll(function(){
		if ( $(this).scrollTop() > headerTopHeight && headerFix.hasClass(\'not-fixed\') ){
		    headerFix.removeClass(\'not-fixed\').addClass(\'header-fixed\');
		} else if($(this).scrollTop() <= headerTopHeight && headerFix.hasClass(\'header-fixed\')) {
			headerFix.removeClass(\'header-fixed\').addClass(\'not-fixed\');
		}
    });
});
</script>',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
        ),
      ),
      'scripts-system' => 
      array (
        'fields' => 
        array (
          'id' => 99,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'scripts-system',
          'description' => '',
          'editor_type' => 0,
          'category' => 12,
          'cache_type' => 0,
          'snippet' => '$(\'.zoom\').magnificPopup({
    type:\'image\'
});
$(\'.zoom-gal\').magnificPopup({
    type:\'image\',
    gallery: {enabled: true}
});
$(\'.modal-window\').magnificPopup({
    type:\'inline\'
});
$.extend(true, $.magnificPopup.defaults, { // перевод для magnific-popup
    tClose: \'Закрыть (Esc)\', // Alt text on close button
    tLoading: \'Загрузка...\', // Text that is displayed during loading. Can contain %curr% and %total% keys
    gallery: {
        tPrev: \'Предыдущий\', // Alt text on left arrow
        tNext: \'Следующий\', // Alt text on right arrow
        tCounter: \'%curr% из %total%\' // Markup for "1 of 7" counter
    },
    image: {
        tError: \'Не удалось загрузить <a href="%url%">изображение</a>.\' // Error message when image could not be loaded
    },
    ajax: {
        tError: \'Не удалось загрузить <a href="%url%">содержимое</a>.\' // Error message when ajax request failed
    }
});
',
          'locked' => false,
          'properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '$(\'.zoom\').magnificPopup({
    type:\'image\'
});
$(\'.zoom-gal\').magnificPopup({
    type:\'image\',
    gallery: {enabled: true}
});
$(\'.modal-window\').magnificPopup({
    type:\'inline\'
});
$.extend(true, $.magnificPopup.defaults, { // перевод для magnific-popup
    tClose: \'Закрыть (Esc)\', // Alt text on close button
    tLoading: \'Загрузка...\', // Text that is displayed during loading. Can contain %curr% and %total% keys
    gallery: {
        tPrev: \'Предыдущий\', // Alt text on left arrow
        tNext: \'Следующий\', // Alt text on right arrow
        tCounter: \'%curr% из %total%\' // Markup for "1 of 7" counter
    },
    image: {
        tError: \'Не удалось загрузить <a href="%url%">изображение</a>.\' // Error message when image could not be loaded
    },
    ajax: {
        tError: \'Не удалось загрузить <a href="%url%">содержимое</a>.\' // Error message when ajax request failed
    }
});
',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'button-to-top' => 
      array (
        'fields' => 
        array (
          'id' => 121,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'button-to-top',
          'description' => '',
          'editor_type' => 0,
          'category' => 12,
          'cache_type' => 0,
          'snippet' => '<script>
    $(function() {
        var toTopButton = $(\'#button-to-top\');
        var edgeValue = 100;
        var wind = $(window);

        $(window).scroll(function(e) {
            toTopButton.toggle(wind.scrollTop() > edgeValue);
        }).trigger(\'scroll\');
        toTopButton.click(function() {
            $(\'html, body\').animate({scrollTop: 0}, 300);
        });
    });
</script>

<div id="button-to-top" class="btn-top-top"><i class="fa fa-chevron-up"></i></div>',
          'locked' => false,
          'properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '<script>
    $(function() {
        var toTopButton = $(\'#button-to-top\');
        var edgeValue = 100;
        var wind = $(window);

        $(window).scroll(function(e) {
            toTopButton.toggle(wind.scrollTop() > edgeValue);
        }).trigger(\'scroll\');
        toTopButton.click(function() {
            $(\'html, body\').animate({scrollTop: 0}, 300);
        });
    });
</script>

<div id="button-to-top" class="btn-top-top"><i class="fa fa-chevron-up"></i></div>',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'footer' => 
      array (
        'fields' => 
        array (
          'id' => 2,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'footer',
          'description' => '',
          'editor_type' => 0,
          'category' => 12,
          'cache_type' => 0,
          'snippet' => '        </main>
        <div class="pushfooter"></div>
    </div>
    <footer class="footer-main">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <span class="copyright">&copy; [[+set.site-name:default=`[[++site_name]]`]] - [[currentYear]]</span>
                </div>
                <div class="col-md-4">
                    [[$share]]
                </div>
                <div class="col-md-4">
                    <div class="footer-links">
                        [[$lirucounter]]
                    </div>
                </div>
            </div
        </div>
    </footer>
    [[$forms]]
    [[$ya-metrika]]
    [[$google-analytics]]
    [[-!viewCounter]]
</body>
</html>',
          'locked' => false,
          'properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '        </main>
        <div class="pushfooter"></div>
    </div>
    <footer class="footer-main">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <span class="copyright">&copy; [[+set.site-name:default=`[[++site_name]]`]] - [[currentYear]]</span>
                </div>
                <div class="col-md-4">
                    [[$share]]
                </div>
                <div class="col-md-4">
                    <div class="footer-links">
                        [[$lirucounter]]
                    </div>
                </div>
            </div
        </div>
    </footer>
    [[$forms]]
    [[$ya-metrika]]
    [[$google-analytics]]
    [[-!viewCounter]]
</body>
</html>',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'share' => 
      array (
        'fields' => 
        array (
          'id' => 102,
          'source' => 0,
          'property_preprocess' => false,
          'name' => 'share',
          'description' => '',
          'editor_type' => 0,
          'category' => 20,
          'cache_type' => 0,
          'snippet' => '<div class="social-block">
    [[!+set.social-buttons:replace=`||==`]]
    [[!+set.msg-buttons:replace=`||==`]]
</div>',
          'locked' => false,
          'properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '<div class="social-block">
    [[!+set.social-buttons:replace=`||==`]]
    [[!+set.msg-buttons:replace=`||==`]]
</div>',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
        ),
      ),
      'lirucounter' => 
      array (
        'fields' => 
        array (
          'id' => 6,
          'source' => 0,
          'property_preprocess' => false,
          'name' => 'lirucounter',
          'description' => 'Счетчик liveinternet.ru',
          'editor_type' => 0,
          'category' => 20,
          'cache_type' => 0,
          'snippet' => '<!--LiveInternet counter--><script type="text/javascript"><!--
document.write("<a href=\'//www.liveinternet.ru/click\' "+
"target=_blank><img src=\'//counter.yadro.ru/hit?t44.[[+color]];r"+
escape(document.referrer)+((typeof(screen)=="undefined")?"":
";s"+screen.width+"*"+screen.height+"*"+(screen.colorDepth?
screen.colorDepth:screen.pixelDepth))+";u"+escape(document.URL)+
";"+Math.random()+
"\' alt=\'\' title=\'LiveInternet\' "+
"border=\'0\' width=\'31\' height=\'31\'><\\/a>")
//--></script><!--/LiveInternet-->',
          'locked' => false,
          'properties' => 
          array (
            'color' => 
            array (
              'name' => 'color',
              'desc' => 'Цвет',
              'type' => 'list',
              'options' => 
              array (
                0 => 
                array (
                  'text' => 'Серый 10%',
                  'value' => '1',
                  'name' => 'Серый 10%',
                ),
                1 => 
                array (
                  'text' => 'Серый 20%',
                  'value' => '2',
                  'name' => 'Серый 20%',
                ),
                2 => 
                array (
                  'text' => 'Серый 40%',
                  'value' => '3',
                  'name' => 'Серый 40%',
                ),
                3 => 
                array (
                  'text' => 'Серый 50%',
                  'value' => '4',
                  'name' => 'Серый 50%',
                ),
                4 => 
                array (
                  'text' => 'Серый 70%',
                  'value' => '5',
                  'name' => 'Серый 70%',
                ),
                5 => 
                array (
                  'text' => 'Жёлто-оранжевый',
                  'value' => '6',
                  'name' => 'Жёлто-оранжевый',
                ),
                6 => 
                array (
                  'text' => 'Оранжевый',
                  'value' => '7',
                  'name' => 'Оранжевый',
                ),
                7 => 
                array (
                  'text' => 'Пурпурный',
                  'value' => '8',
                  'name' => 'Пурпурный',
                ),
                8 => 
                array (
                  'text' => 'Фиолетовый',
                  'value' => '9',
                  'name' => 'Фиолетовый',
                ),
                9 => 
                array (
                  'text' => 'Серо-голубой',
                  'value' => '10',
                  'name' => 'Серо-голубой',
                ),
                10 => 
                array (
                  'text' => 'Синий',
                  'value' => '11',
                  'name' => 'Синий',
                ),
                11 => 
                array (
                  'text' => 'Салатовый',
                  'value' => '12',
                  'name' => 'Салатовый',
                ),
                12 => 
                array (
                  'text' => 'Изумрудный',
                  'value' => '13',
                  'name' => 'Изумрудный',
                ),
                13 => 
                array (
                  'text' => 'Ярко-зелёный',
                  'value' => '14',
                  'name' => 'Ярко-зелёный',
                ),
                14 => 
                array (
                  'text' => 'Зелёный',
                  'value' => '15',
                  'name' => 'Зелёный',
                ),
                15 => 
                array (
                  'text' => 'Жёлто-зелёный',
                  'value' => '16',
                  'name' => 'Жёлто-зелёный',
                ),
                16 => 
                array (
                  'text' => 'Жёлтый',
                  'value' => '17',
                  'name' => 'Жёлтый',
                ),
                17 => 
                array (
                  'text' => 'Поросячий-розовый',
                  'value' => '18',
                  'name' => 'Поросячий-розовый',
                ),
              ),
              'value' => '6',
              'lexicon' => NULL,
              'area' => '',
              'desc_trans' => 'Цвет',
              'area_trans' => '',
            ),
          ),
          'static' => false,
          'static_file' => '',
          'content' => '<!--LiveInternet counter--><script type="text/javascript"><!--
document.write("<a href=\'//www.liveinternet.ru/click\' "+
"target=_blank><img src=\'//counter.yadro.ru/hit?t44.[[+color]];r"+
escape(document.referrer)+((typeof(screen)=="undefined")?"":
";s"+screen.width+"*"+screen.height+"*"+(screen.colorDepth?
screen.colorDepth:screen.pixelDepth))+";u"+escape(document.URL)+
";"+Math.random()+
"\' alt=\'\' title=\'LiveInternet\' "+
"border=\'0\' width=\'31\' height=\'31\'><\\/a>")
//--></script><!--/LiveInternet-->',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
        ),
      ),
      'forms' => 
      array (
        'fields' => 
        array (
          'id' => 127,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'forms',
          'description' => '',
          'editor_type' => 0,
          'category' => 12,
          'cache_type' => 0,
          'snippet' => '<div id="order-form" class="mfp-hide modal-block">
    <div class="modal-block-name">Заказать звонок</div>
    [[$call-back-form-ajax]]
</div>

<div id="order-form2" class="mfp-hide modal-block">
    <div class="modal-block-name">Перезвоните мне прямо сейчас</div>
    [[$call-back-form-ajax]]
</div>',
          'locked' => false,
          'properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '<div id="order-form" class="mfp-hide modal-block">
    <div class="modal-block-name">Заказать звонок</div>
    [[$call-back-form-ajax]]
</div>

<div id="order-form2" class="mfp-hide modal-block">
    <div class="modal-block-name">Перезвоните мне прямо сейчас</div>
    [[$call-back-form-ajax]]
</div>',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'call-back-form-ajax' => 
      array (
        'fields' => 
        array (
          'id' => 125,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'call-back-form-ajax',
          'description' => '',
          'editor_type' => 0,
          'category' => 11,
          'cache_type' => 0,
          'snippet' => '[[!AjaxForm@ajaxForm?
    &snippet=`FormIt`
    &form=`call-back-form`
    &hooks=`spam,email`
    &emailTpl=`call-back-email-report` 
    &emailTo=`[[+set.recipient-email:default=`[[++emailsender]]`]]` 
    &emailSubject=`Сообщение с сайта [[++site_name]]`
    &emailFrom=`webmaster@[[domain]]`
    &validate=`name:required, phone:required`
    &validationErrorMessage=`В форме содержатся ошибки!`
    &successMessage=`Сообщение успешно отправлено`
]]',
          'locked' => false,
          'properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '[[!AjaxForm@ajaxForm?
    &snippet=`FormIt`
    &form=`call-back-form`
    &hooks=`spam,email`
    &emailTpl=`call-back-email-report` 
    &emailTo=`[[+set.recipient-email:default=`[[++emailsender]]`]]` 
    &emailSubject=`Сообщение с сайта [[++site_name]]`
    &emailFrom=`webmaster@[[domain]]`
    &validate=`name:required, phone:required`
    &validationErrorMessage=`В форме содержатся ошибки!`
    &successMessage=`Сообщение успешно отправлено`
]]',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'ya-metrika' => 
      array (
        'fields' => 
        array (
          'id' => 101,
          'source' => 0,
          'property_preprocess' => false,
          'name' => 'ya-metrika',
          'description' => '',
          'editor_type' => 0,
          'category' => 20,
          'cache_type' => 0,
          'snippet' => '[[+set.ya-counter-id:notempty=`
    <!-- Yandex.Metrika counter -->
    <script type="text/javascript">
        (function (d, w, c) {
            (w[c] = w[c] || []).push(function() {
                try {
                    w.yaCounter[[+set.ya-counter-id]] = new Ya.Metrika({
                        id:[[+set.ya-counter-id]],
                        clickmap:true,
                        trackLinks:true,
                        accurateTrackBounce:true,
                        webvisor:true
                    });
                } catch(e) { }
            });
    
            var n = d.getElementsByTagName("script")[0],
                s = d.createElement("script"),
                f = function () { n.parentNode.insertBefore(s, n); };
            s.type = "text/javascript";
            s.async = true;
            s.src = "https://mc.yandex.ru/metrika/watch.js";
    
            if (w.opera == "[object Opera]") {
                d.addEventListener("DOMContentLoaded", f, false);
            } else { f(); }
        })(document, window, "yandex_metrika_callbacks");
    </script>
    <noscript><div><img src="https://mc.yandex.ru/watch/[[+set.ya-counter-id]]" style="position:absolute; left:-9999px;" alt="" /></div></noscript>
    <!-- /Yandex.Metrika counter -->
`]]
',
          'locked' => false,
          'properties' => NULL,
          'static' => false,
          'static_file' => '',
          'content' => '[[+set.ya-counter-id:notempty=`
    <!-- Yandex.Metrika counter -->
    <script type="text/javascript">
        (function (d, w, c) {
            (w[c] = w[c] || []).push(function() {
                try {
                    w.yaCounter[[+set.ya-counter-id]] = new Ya.Metrika({
                        id:[[+set.ya-counter-id]],
                        clickmap:true,
                        trackLinks:true,
                        accurateTrackBounce:true,
                        webvisor:true
                    });
                } catch(e) { }
            });
    
            var n = d.getElementsByTagName("script")[0],
                s = d.createElement("script"),
                f = function () { n.parentNode.insertBefore(s, n); };
            s.type = "text/javascript";
            s.async = true;
            s.src = "https://mc.yandex.ru/metrika/watch.js";
    
            if (w.opera == "[object Opera]") {
                d.addEventListener("DOMContentLoaded", f, false);
            } else { f(); }
        })(document, window, "yandex_metrika_callbacks");
    </script>
    <noscript><div><img src="https://mc.yandex.ru/watch/[[+set.ya-counter-id]]" style="position:absolute; left:-9999px;" alt="" /></div></noscript>
    <!-- /Yandex.Metrika counter -->
`]]
',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
        ),
      ),
      'google-analytics' => 
      array (
        'fields' => 
        array (
          'id' => 100,
          'source' => 0,
          'property_preprocess' => false,
          'name' => 'google-analytics',
          'description' => '',
          'editor_type' => 0,
          'category' => 20,
          'cache_type' => 0,
          'snippet' => '[[+set.ga-id:notempty=`
    <script>
      (function(i,s,o,g,r,a,m){i[\'GoogleAnalyticsObject\']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,\'script\',\'//www.google-analytics.com/analytics.js\',\'ga\');
    
      ga(\'create\', \'[[+set.ga-id]]\', \'auto\');
      ga(\'send\', \'pageview\');
    
    </script>
`]]',
          'locked' => false,
          'properties' => NULL,
          'static' => false,
          'static_file' => '',
          'content' => '[[+set.ga-id:notempty=`
    <script>
      (function(i,s,o,g,r,a,m){i[\'GoogleAnalyticsObject\']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,\'script\',\'//www.google-analytics.com/analytics.js\',\'ga\');
    
      ga(\'create\', \'[[+set.ga-id]]\', \'auto\');
      ga(\'send\', \'pageview\');
    
    </script>
`]]',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
        ),
      ),
    ),
    'modSnippet' => 
    array (
      'pdoMenu' => 
      array (
        'fields' => 
        array (
          'id' => 27,
          'source' => 1,
          'property_preprocess' => false,
          'name' => 'pdoMenu',
          'description' => '',
          'editor_type' => 0,
          'category' => 7,
          'cache_type' => 0,
          'snippet' => '/** @var array $scriptProperties */

// Convert parameters from Wayfinder if exists
if (isset($startId)) {
    $scriptProperties[\'parents\'] = $startId;
}
if (!empty($includeDocs)) {
    $tmp = array_map(\'trim\', explode(\',\', $includeDocs));
    foreach ($tmp as $v) {
        if (!empty($scriptProperties[\'resources\'])) {
            $scriptProperties[\'resources\'] .= \',\' . $v;
        } else {
            $scriptProperties[\'resources\'] = $v;
        }
    }
}
if (!empty($excludeDocs)) {
    $tmp = array_map(\'trim\', explode(\',\', $excludeDocs));
    foreach ($tmp as $v) {
        if (!empty($scriptProperties[\'resources\'])) {
            $scriptProperties[\'resources\'] .= \',-\' . $v;
        } else {
            $scriptProperties[\'resources\'] = \'-\' . $v;
        }
    }
}

if (!empty($previewUnpublished) && $modx->hasPermission(\'view_unpublished\')) {
    $scriptProperties[\'showUnpublished\'] = 1;
}

$scriptProperties[\'depth\'] = empty($level) ? 100 : abs($level) - 1;
if (!empty($contexts)) {
    $scriptProperties[\'context\'] = $contexts;
}
if (empty($scriptProperties[\'context\'])) {
    $scriptProperties[\'context\'] = $modx->resource->context_key;
}

// Save original parents specified by user
$specified_parents = array_map(\'trim\', explode(\',\', $scriptProperties[\'parents\']));

if ($scriptProperties[\'parents\'] === \'\') {
    $scriptProperties[\'parents\'] = $modx->resource->id;
} elseif ($scriptProperties[\'parents\'] === 0 || $scriptProperties[\'parents\'] === \'0\') {
    if ($scriptProperties[\'depth\'] !== \'\' && $scriptProperties[\'depth\'] !== 100) {
        $contexts = array_map(\'trim\', explode(\',\', $scriptProperties[\'context\']));
        $parents = array();
        if (!empty($scriptProperties[\'showDeleted\'])) {
            $pdoFetch = $modx->getService(\'pdoFetch\');
            foreach ($contexts as $ctx) {
                $parents = array_merge($parents,
                    $pdoFetch->getChildIds(\'modResource\', 0, $scriptProperties[\'depth\'], array(\'context\' => $ctx)));
            }
        } else {
            foreach ($contexts as $ctx) {
                $parents = array_merge($parents,
                    $modx->getChildIds(0, $scriptProperties[\'depth\'], array(\'context\' => $ctx)));
            }
        }
        $scriptProperties[\'parents\'] = !empty($parents) ? implode(\',\', $parents) : \'+0\';
        $scriptProperties[\'depth\'] = 0;
    }
    $scriptProperties[\'includeParents\'] = 1;
    $scriptProperties[\'displayStart\'] = 0;
} else {
    $parents = array_map(\'trim\', explode(\',\', $scriptProperties[\'parents\']));
    $parents_in = $parents_out = array();
    foreach ($parents as $v) {
        if (!is_numeric($v)) {
            continue;
        }
        if ($v[0] == \'-\') {
            $parents_out[] = abs($v);
        } else {
            $parents_in[] = abs($v);
        }
    }

    if (empty($parents_in)) {
        $scriptProperties[\'includeParents\'] = 1;
        $scriptProperties[\'displayStart\'] = 0;
    }
}

if (!empty($displayStart)) {
    $scriptProperties[\'includeParents\'] = 1;
}
if (!empty($ph)) {
    $toPlaceholder = $ph;
}
if (!empty($sortOrder)) {
    $scriptProperties[\'sortdir\'] = $sortOrder;
}
if (!empty($sortBy)) {
    $scriptProperties[\'sortby\'] = $sortBy;
}
if (!empty($permissions)) {
    $scriptProperties[\'checkPermissions\'] = $permissions;
}
if (!empty($cacheResults)) {
    $scriptProperties[\'cache\'] = $cacheResults;
}
if (!empty($ignoreHidden)) {
    $scriptProperties[\'showHidden\'] = $ignoreHidden;
}

$wfTemplates = array(
    \'outerTpl\' => \'tplOuter\',
    \'rowTpl\' => \'tpl\',
    \'parentRowTpl\' => \'tplParentRow\',
    \'parentRowHereTpl\' => \'tplParentRowHere\',
    \'hereTpl\' => \'tplHere\',
    \'innerTpl\' => \'tplInner\',
    \'innerRowTpl\' => \'tplInnerRow\',
    \'innerHereTpl\' => \'tplInnerHere\',
    \'activeParentRowTpl\' => \'tplParentRowActive\',
    \'categoryFoldersTpl\' => \'tplCategoryFolder\',
    \'startItemTpl\' => \'tplStart\',
);
foreach ($wfTemplates as $k => $v) {
    if (isset(${$k})) {
        $scriptProperties[$v] = ${$k};
    }
}
//---

/** @var pdoMenu $pdoMenu */
$fqn = $modx->getOption(\'pdoMenu.class\', null, \'pdotools.pdomenu\', true);
$path = $modx->getOption(\'pdomenu_class_path\', null, MODX_CORE_PATH . \'components/pdotools/model/\', true);
if ($pdoClass = $modx->loadClass($fqn, $path, false, true)) {
    $pdoMenu = new $pdoClass($modx, $scriptProperties);
} else {
    return false;
}
$pdoMenu->pdoTools->addTime(\'pdoTools loaded\');

$cache = !empty($cache) || (!$modx->user->id && !empty($cacheAnonymous));
if (empty($scriptProperties[\'cache_key\'])) {
    $scriptProperties[\'cache_key\'] = \'pdomenu/\' . sha1(serialize($scriptProperties));
}

$output = \'\';
$tree = array();
if ($cache) {
    $tree = $pdoMenu->pdoTools->getCache($scriptProperties);
}
if (empty($tree)) {
    $data = $pdoMenu->pdoTools->run();
    $data = $pdoMenu->pdoTools->buildTree($data, \'id\', \'parent\', $specified_parents);
    $tree = array();
    foreach ($data as $k => $v) {
        if (empty($v[\'id\'])) {
            if (!in_array($k, $specified_parents) && !$pdoMenu->checkResource($k)) {
                continue;
            } else {
                $tree = array_merge($tree, $v[\'children\']);
            }
        } else {
            $tree[$k] = $v;
        }
    }
    if ($cache) {
        $pdoMenu->pdoTools->setCache($tree, $scriptProperties);
    }
}
if (!empty($tree)) {
    $output = $pdoMenu->templateTree($tree);
}

if ($modx->user->hasSessionContext(\'mgr\') && !empty($showLog)) {
    $output .= \'<pre class="pdoMenuLog">\' . print_r($pdoMenu->pdoTools->getTime(), 1) . \'</pre>\';
}

if (!empty($toPlaceholder)) {
    $modx->setPlaceholder($toPlaceholder, $output);
} else {
    return $output;
}',
          'locked' => false,
          'properties' => 
          array (
            'showLog' => 
            array (
              'name' => 'showLog',
              'desc' => 'pdotools_prop_showLog',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Показывать дополнительную информацию о работе сниппета. Только для авторизованных в контекте "mgr".',
              'area_trans' => '',
            ),
            'fastMode' => 
            array (
              'name' => 'fastMode',
              'desc' => 'pdotools_prop_fastMode',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Быстрый режим обработки чанков. Все необработанные теги (условия, сниппеты и т.п.) будут вырезаны.',
              'area_trans' => '',
            ),
            'level' => 
            array (
              'name' => 'level',
              'desc' => 'pdotools_prop_level',
              'type' => 'numberfield',
              'options' => 
              array (
              ),
              'value' => 0,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Уровень генерируемого меню.',
              'area_trans' => '',
            ),
            'parents' => 
            array (
              'name' => 'parents',
              'desc' => 'pdotools_prop_parents',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Список родителей, через запятую, для поиска результатов. По умолчанию выборка ограничена текущим родителем. Если поставить 0 - выборка не ограничивается. Если id родителя начинается с дефиса, он и его потомки исключается из выборки.',
              'area_trans' => '',
            ),
            'displayStart' => 
            array (
              'name' => 'displayStart',
              'desc' => 'pdotools_prop_displayStart',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Включить показ начальных узлов меню. Полезно при указании более одного "parents".',
              'area_trans' => '',
            ),
            'resources' => 
            array (
              'name' => 'resources',
              'desc' => 'pdotools_prop_resources',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Список ресурсов, через запятую, для вывода в результатах. Если id ресурса начинается с дефиса, этот ресурс исключается из выборки.',
              'area_trans' => '',
            ),
            'templates' => 
            array (
              'name' => 'templates',
              'desc' => 'pdotools_prop_templates',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Список шаблонов, через запятую, для фильтрации результатов. Если id шаблона начинается с дефиса, ресурсы с ним исключается из выборки.',
              'area_trans' => '',
            ),
            'context' => 
            array (
              'name' => 'context',
              'desc' => 'pdotools_prop_context',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Ограничение выборки по контексту ресурсов.',
              'area_trans' => '',
            ),
            'cache' => 
            array (
              'name' => 'cache',
              'desc' => 'pdotools_prop_cache',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Кэширование результатов работы сниппета.',
              'area_trans' => '',
            ),
            'cacheTime' => 
            array (
              'name' => 'cacheTime',
              'desc' => 'pdotools_prop_cacheTime',
              'type' => 'numberfield',
              'options' => 
              array (
              ),
              'value' => 3600,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Время актуальности кэша в секундах.',
              'area_trans' => '',
            ),
            'cacheAnonymous' => 
            array (
              'name' => 'cacheAnonymous',
              'desc' => 'pdotools_prop_cacheAnonymous',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Включить кэширование только для неавторизованных посетителей.',
              'area_trans' => '',
            ),
            'plPrefix' => 
            array (
              'name' => 'plPrefix',
              'desc' => 'pdotools_prop_plPrefix',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'wf.',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Префикс для выставляемых плейсхолдеров, по умолчанию "wf.".',
              'area_trans' => '',
            ),
            'showHidden' => 
            array (
              'name' => 'showHidden',
              'desc' => 'pdotools_prop_showHidden',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Показывать ресурсы, скрытые в меню.',
              'area_trans' => '',
            ),
            'showUnpublished' => 
            array (
              'name' => 'showUnpublished',
              'desc' => 'pdotools_prop_showUnpublished',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Показывать неопубликованные ресурсы.',
              'area_trans' => '',
            ),
            'showDeleted' => 
            array (
              'name' => 'showDeleted',
              'desc' => 'pdotools_prop_showDeleted',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Показывать удалённые ресурсы.',
              'area_trans' => '',
            ),
            'previewUnpublished' => 
            array (
              'name' => 'previewUnpublished',
              'desc' => 'pdotools_prop_previewUnpublished',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Включить показ неопубликованных документов, если у пользователя есть на это разрешение.',
              'area_trans' => '',
            ),
            'hideSubMenus' => 
            array (
              'name' => 'hideSubMenus',
              'desc' => 'pdotools_prop_hideSubMenus',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Спрятать неактивные ветки меню.',
              'area_trans' => '',
            ),
            'useWeblinkUrl' => 
            array (
              'name' => 'useWeblinkUrl',
              'desc' => 'pdotools_prop_useWeblinkUrl',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => true,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Генерировать ссылку с учетом класса ресурса.',
              'area_trans' => '',
            ),
            'sortdir' => 
            array (
              'name' => 'sortdir',
              'desc' => 'pdotools_prop_sortdir',
              'type' => 'list',
              'options' => 
              array (
                0 => 
                array (
                  'text' => 'ASC',
                  'value' => 'ASC',
                  'name' => 'ASC',
                ),
                1 => 
                array (
                  'text' => 'DESC',
                  'value' => 'DESC',
                  'name' => 'DESC',
                ),
              ),
              'value' => 'ASC',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Направление сортировки: по убыванию или возрастанию.',
              'area_trans' => '',
            ),
            'sortby' => 
            array (
              'name' => 'sortby',
              'desc' => 'pdotools_prop_sortby',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'menuindex',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Любое поле ресурса для сортировки, включая ТВ параметр, если он указан в параметре "includeTVs". Можно указывать JSON строку с массивом нескольких полей. Для случайно сортировки укажите "RAND()"',
              'area_trans' => '',
            ),
            'limit' => 
            array (
              'name' => 'limit',
              'desc' => 'pdotools_prop_limit',
              'type' => 'numberfield',
              'options' => 
              array (
              ),
              'value' => 0,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Ограничение количества результатов выборки. Можно использовать "0".',
              'area_trans' => '',
            ),
            'offset' => 
            array (
              'name' => 'offset',
              'desc' => 'pdotools_prop_offset',
              'type' => 'numberfield',
              'options' => 
              array (
              ),
              'value' => 0,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Пропуск результатов от начала.',
              'area_trans' => '',
            ),
            'rowIdPrefix' => 
            array (
              'name' => 'rowIdPrefix',
              'desc' => 'pdotools_prop_rowIdPrefix',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Префикс id="" для выставления идентификатора в чанк.',
              'area_trans' => '',
            ),
            'firstClass' => 
            array (
              'name' => 'firstClass',
              'desc' => 'pdotools_prop_firstClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'first',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс для первого пункта меню.',
              'area_trans' => '',
            ),
            'lastClass' => 
            array (
              'name' => 'lastClass',
              'desc' => 'pdotools_prop_lastClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'last',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс последнего пункта меню.',
              'area_trans' => '',
            ),
            'hereClass' => 
            array (
              'name' => 'hereClass',
              'desc' => 'pdotools_prop_hereClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'active',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс для активного пункта меню.',
              'area_trans' => '',
            ),
            'parentClass' => 
            array (
              'name' => 'parentClass',
              'desc' => 'pdotools_prop_parentClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс категории меню.',
              'area_trans' => '',
            ),
            'rowClass' => 
            array (
              'name' => 'rowClass',
              'desc' => 'pdotools_prop_rowClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс одной строки меню.',
              'area_trans' => '',
            ),
            'outerClass' => 
            array (
              'name' => 'outerClass',
              'desc' => 'pdotools_prop_outerClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс обертки меню.',
              'area_trans' => '',
            ),
            'innerClass' => 
            array (
              'name' => 'innerClass',
              'desc' => 'pdotools_prop_innerClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс внутренних ссылок меню.',
              'area_trans' => '',
            ),
            'levelClass' => 
            array (
              'name' => 'levelClass',
              'desc' => 'pdotools_prop_levelClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс уровня меню. Например, если укажите "level", то будет "level1", "level2" и т.д.',
              'area_trans' => '',
            ),
            'selfClass' => 
            array (
              'name' => 'selfClass',
              'desc' => 'pdotools_prop_selfClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс текущего документа в меню.',
              'area_trans' => '',
            ),
            'webLinkClass' => 
            array (
              'name' => 'webLinkClass',
              'desc' => 'pdotools_prop_webLinkClass',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Класс документа-ссылки.',
              'area_trans' => '',
            ),
            'tplOuter' => 
            array (
              'name' => 'tplOuter',
              'desc' => 'pdotools_prop_tplOuter',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '@INLINE <ul[[+classes]]>[[+wrapper]]</ul>',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Чанк-обёртка всего блока меню.',
              'area_trans' => '',
            ),
            'tpl' => 
            array (
              'name' => 'tpl',
              'desc' => 'pdotools_prop_tpl',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '@INLINE <li[[+classes]]><a href="[[+link]]" [[+attributes]]>[[+menutitle]]</a>[[+wrapper]]</li>',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Имя чанка для оформления ресурса. Если не указан, то содержимое полей ресурса будет распечатано на экран.',
              'area_trans' => '',
            ),
            'tplParentRow' => 
            array (
              'name' => 'tplParentRow',
              'desc' => 'pdotools_prop_tplParentRow',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Чанк оформления контейнера с потомками.',
              'area_trans' => '',
            ),
            'tplParentRowHere' => 
            array (
              'name' => 'tplParentRowHere',
              'desc' => 'pdotools_prop_tplParentRowHere',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Чанк оформления текущего контейнера с потомками.',
              'area_trans' => '',
            ),
            'tplHere' => 
            array (
              'name' => 'tplHere',
              'desc' => 'pdotools_prop_tplHere',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Чанк текущего документа',
              'area_trans' => '',
            ),
            'tplInner' => 
            array (
              'name' => 'tplInner',
              'desc' => 'pdotools_prop_tplInner',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Чанк-обёртка внутренних пунктов меню. Если пуст - будет использовать "tplInner".',
              'area_trans' => '',
            ),
            'tplInnerRow' => 
            array (
              'name' => 'tplInnerRow',
              'desc' => 'pdotools_prop_tplInnerRow',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Чанк-обёртка активного пункта меню.',
              'area_trans' => '',
            ),
            'tplInnerHere' => 
            array (
              'name' => 'tplInnerHere',
              'desc' => 'pdotools_prop_tplInnerHere',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Чанк-обёртка активного пункта меню.',
              'area_trans' => '',
            ),
            'tplParentRowActive' => 
            array (
              'name' => 'tplParentRowActive',
              'desc' => 'pdotools_prop_tplParentRowActive',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Чанк оформления активного контейнера с потомками.',
              'area_trans' => '',
            ),
            'tplCategoryFolder' => 
            array (
              'name' => 'tplCategoryFolder',
              'desc' => 'pdotools_prop_tplCategoryFolder',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Специальный чанк оформления категории. Категория - это документ с потомками и или нулевым шаблоном, или с атрибутом "rel=\\"category\\"".',
              'area_trans' => '',
            ),
            'tplStart' => 
            array (
              'name' => 'tplStart',
              'desc' => 'pdotools_prop_tplStart',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '@INLINE <h2[[+classes]]>[[+menutitle]]</h2>[[+wrapper]]',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Чанк оформления корневого пункта, при условии, что включен "displayStart".',
              'area_trans' => '',
            ),
            'checkPermissions' => 
            array (
              'name' => 'checkPermissions',
              'desc' => 'pdotools_prop_checkPermissions',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Укажите, какие разрешения нужно проверять у пользователя при выводе документов.',
              'area_trans' => '',
            ),
            'hereId' => 
            array (
              'name' => 'hereId',
              'desc' => 'pdotools_prop_hereId',
              'type' => 'numberfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Id документа, текущего для генерируемого меню. Нужно указывать только если скрипт сам его неверно определяет, например при выводе меню из чанка другого сниппета.',
              'area_trans' => '',
            ),
            'where' => 
            array (
              'name' => 'where',
              'desc' => 'pdotools_prop_where',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Массив дополнительных параметров выборки, закодированный в JSON.',
              'area_trans' => '',
            ),
            'select' => 
            array (
              'name' => 'select',
              'desc' => 'pdotools_prop_select',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Список полей для выборки, через запятую. Можно указывать JSON строку с массивом, например {"modResource":"id,pagetitle,content"}.',
              'area_trans' => '',
            ),
            'scheme' => 
            array (
              'name' => 'scheme',
              'desc' => 'pdotools_prop_scheme',
              'type' => 'list',
              'options' => 
              array (
                0 => 
                array (
                  'value' => '',
                  'text' => 'System default',
                  'name' => 'System default',
                ),
                1 => 
                array (
                  'value' => -1,
                  'text' => '-1 (relative to site_url)',
                  'name' => '-1 (relative to site_url)',
                ),
                2 => 
                array (
                  'value' => 'full',
                  'text' => 'full (absolute, prepended with site_url)',
                  'name' => 'full (absolute, prepended with site_url)',
                ),
                3 => 
                array (
                  'value' => 'abs',
                  'text' => 'abs (absolute, prepended with base_url)',
                  'name' => 'abs (absolute, prepended with base_url)',
                ),
                4 => 
                array (
                  'value' => 'http',
                  'text' => 'http (absolute, forced to http scheme)',
                  'name' => 'http (absolute, forced to http scheme)',
                ),
                5 => 
                array (
                  'value' => 'https',
                  'text' => 'https (absolute, forced to https scheme)',
                  'name' => 'https (absolute, forced to https scheme)',
                ),
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Схема формирования ссылок: "uri" для подстановки поля uri документа (очень быстро) или параметр для modX::makeUrl().',
              'area_trans' => '',
            ),
            'toPlaceholder' => 
            array (
              'name' => 'toPlaceholder',
              'desc' => 'pdotools_prop_toPlaceholder',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '',
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Если не пусто, сниппет сохранит все данные в плейсхолдер с этим именем, вместо вывода не экран.',
              'area_trans' => '',
            ),
            'countChildren' => 
            array (
              'name' => 'countChildren',
              'desc' => 'pdotools_prop_countChildren',
              'type' => 'combo-boolean',
              'options' => 
              array (
              ),
              'value' => false,
              'lexicon' => 'pdotools:properties',
              'area' => '',
              'desc_trans' => 'Вывести точное количество активных потомков документа в плейсхолдер [[+children]].',
              'area_trans' => '',
            ),
          ),
          'moduleguid' => '',
          'static' => false,
          'static_file' => 'core/components/pdotools/elements/snippets/snippet.pdomenu.php',
          'content' => '/** @var array $scriptProperties */

// Convert parameters from Wayfinder if exists
if (isset($startId)) {
    $scriptProperties[\'parents\'] = $startId;
}
if (!empty($includeDocs)) {
    $tmp = array_map(\'trim\', explode(\',\', $includeDocs));
    foreach ($tmp as $v) {
        if (!empty($scriptProperties[\'resources\'])) {
            $scriptProperties[\'resources\'] .= \',\' . $v;
        } else {
            $scriptProperties[\'resources\'] = $v;
        }
    }
}
if (!empty($excludeDocs)) {
    $tmp = array_map(\'trim\', explode(\',\', $excludeDocs));
    foreach ($tmp as $v) {
        if (!empty($scriptProperties[\'resources\'])) {
            $scriptProperties[\'resources\'] .= \',-\' . $v;
        } else {
            $scriptProperties[\'resources\'] = \'-\' . $v;
        }
    }
}

if (!empty($previewUnpublished) && $modx->hasPermission(\'view_unpublished\')) {
    $scriptProperties[\'showUnpublished\'] = 1;
}

$scriptProperties[\'depth\'] = empty($level) ? 100 : abs($level) - 1;
if (!empty($contexts)) {
    $scriptProperties[\'context\'] = $contexts;
}
if (empty($scriptProperties[\'context\'])) {
    $scriptProperties[\'context\'] = $modx->resource->context_key;
}

// Save original parents specified by user
$specified_parents = array_map(\'trim\', explode(\',\', $scriptProperties[\'parents\']));

if ($scriptProperties[\'parents\'] === \'\') {
    $scriptProperties[\'parents\'] = $modx->resource->id;
} elseif ($scriptProperties[\'parents\'] === 0 || $scriptProperties[\'parents\'] === \'0\') {
    if ($scriptProperties[\'depth\'] !== \'\' && $scriptProperties[\'depth\'] !== 100) {
        $contexts = array_map(\'trim\', explode(\',\', $scriptProperties[\'context\']));
        $parents = array();
        if (!empty($scriptProperties[\'showDeleted\'])) {
            $pdoFetch = $modx->getService(\'pdoFetch\');
            foreach ($contexts as $ctx) {
                $parents = array_merge($parents,
                    $pdoFetch->getChildIds(\'modResource\', 0, $scriptProperties[\'depth\'], array(\'context\' => $ctx)));
            }
        } else {
            foreach ($contexts as $ctx) {
                $parents = array_merge($parents,
                    $modx->getChildIds(0, $scriptProperties[\'depth\'], array(\'context\' => $ctx)));
            }
        }
        $scriptProperties[\'parents\'] = !empty($parents) ? implode(\',\', $parents) : \'+0\';
        $scriptProperties[\'depth\'] = 0;
    }
    $scriptProperties[\'includeParents\'] = 1;
    $scriptProperties[\'displayStart\'] = 0;
} else {
    $parents = array_map(\'trim\', explode(\',\', $scriptProperties[\'parents\']));
    $parents_in = $parents_out = array();
    foreach ($parents as $v) {
        if (!is_numeric($v)) {
            continue;
        }
        if ($v[0] == \'-\') {
            $parents_out[] = abs($v);
        } else {
            $parents_in[] = abs($v);
        }
    }

    if (empty($parents_in)) {
        $scriptProperties[\'includeParents\'] = 1;
        $scriptProperties[\'displayStart\'] = 0;
    }
}

if (!empty($displayStart)) {
    $scriptProperties[\'includeParents\'] = 1;
}
if (!empty($ph)) {
    $toPlaceholder = $ph;
}
if (!empty($sortOrder)) {
    $scriptProperties[\'sortdir\'] = $sortOrder;
}
if (!empty($sortBy)) {
    $scriptProperties[\'sortby\'] = $sortBy;
}
if (!empty($permissions)) {
    $scriptProperties[\'checkPermissions\'] = $permissions;
}
if (!empty($cacheResults)) {
    $scriptProperties[\'cache\'] = $cacheResults;
}
if (!empty($ignoreHidden)) {
    $scriptProperties[\'showHidden\'] = $ignoreHidden;
}

$wfTemplates = array(
    \'outerTpl\' => \'tplOuter\',
    \'rowTpl\' => \'tpl\',
    \'parentRowTpl\' => \'tplParentRow\',
    \'parentRowHereTpl\' => \'tplParentRowHere\',
    \'hereTpl\' => \'tplHere\',
    \'innerTpl\' => \'tplInner\',
    \'innerRowTpl\' => \'tplInnerRow\',
    \'innerHereTpl\' => \'tplInnerHere\',
    \'activeParentRowTpl\' => \'tplParentRowActive\',
    \'categoryFoldersTpl\' => \'tplCategoryFolder\',
    \'startItemTpl\' => \'tplStart\',
);
foreach ($wfTemplates as $k => $v) {
    if (isset(${$k})) {
        $scriptProperties[$v] = ${$k};
    }
}
//---

/** @var pdoMenu $pdoMenu */
$fqn = $modx->getOption(\'pdoMenu.class\', null, \'pdotools.pdomenu\', true);
$path = $modx->getOption(\'pdomenu_class_path\', null, MODX_CORE_PATH . \'components/pdotools/model/\', true);
if ($pdoClass = $modx->loadClass($fqn, $path, false, true)) {
    $pdoMenu = new $pdoClass($modx, $scriptProperties);
} else {
    return false;
}
$pdoMenu->pdoTools->addTime(\'pdoTools loaded\');

$cache = !empty($cache) || (!$modx->user->id && !empty($cacheAnonymous));
if (empty($scriptProperties[\'cache_key\'])) {
    $scriptProperties[\'cache_key\'] = \'pdomenu/\' . sha1(serialize($scriptProperties));
}

$output = \'\';
$tree = array();
if ($cache) {
    $tree = $pdoMenu->pdoTools->getCache($scriptProperties);
}
if (empty($tree)) {
    $data = $pdoMenu->pdoTools->run();
    $data = $pdoMenu->pdoTools->buildTree($data, \'id\', \'parent\', $specified_parents);
    $tree = array();
    foreach ($data as $k => $v) {
        if (empty($v[\'id\'])) {
            if (!in_array($k, $specified_parents) && !$pdoMenu->checkResource($k)) {
                continue;
            } else {
                $tree = array_merge($tree, $v[\'children\']);
            }
        } else {
            $tree[$k] = $v;
        }
    }
    if ($cache) {
        $pdoMenu->pdoTools->setCache($tree, $scriptProperties);
    }
}
if (!empty($tree)) {
    $output = $pdoMenu->templateTree($tree);
}

if ($modx->user->hasSessionContext(\'mgr\') && !empty($showLog)) {
    $output .= \'<pre class="pdoMenuLog">\' . print_r($pdoMenu->pdoTools->getTime(), 1) . \'</pre>\';
}

if (!empty($toPlaceholder)) {
    $modx->setPlaceholder($toPlaceholder, $output);
} else {
    return $output;
}',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 1,
          'name' => 'Filesystem',
          'description' => '',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
          ),
          'is_stream' => true,
        ),
      ),
      'currentYear' => 
      array (
        'fields' => 
        array (
          'id' => 70,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'currentYear',
          'description' => '',
          'editor_type' => 0,
          'category' => 5,
          'cache_type' => 0,
          'snippet' => 'return date(\'Y\');',
          'locked' => false,
          'properties' => 
          array (
          ),
          'moduleguid' => '',
          'static' => false,
          'static_file' => '',
          'content' => 'return date(\'Y\');',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'domain' => 
      array (
        'fields' => 
        array (
          'id' => 65,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'domain',
          'description' => '',
          'editor_type' => 0,
          'category' => 5,
          'cache_type' => 0,
          'snippet' => 'return preg_replace(\'/^www./\', \'\', $_SERVER[\'SERVER_NAME\']);',
          'locked' => false,
          'properties' => 
          array (
          ),
          'moduleguid' => '',
          'static' => false,
          'static_file' => '',
          'content' => 'return preg_replace(\'/^www./\', \'\', $_SERVER[\'SERVER_NAME\']);',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'getSiteSettings' => 
      array (
        'fields' => 
        array (
          'id' => 40,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'getSiteSettings',
          'description' => '',
          'editor_type' => 0,
          'category' => 5,
          'cache_type' => 0,
          'snippet' => '/*
getSiteSettings ver: 1.9
Igor Siluyanov, 16.11.2016

Скрипт получает все значения доп. полей ресурса, содержащего настройки, и создаёт одноимённые плейсхолдеры с префиксом "set.", пример:
[[+set.site-slogan]]
Контакты хранятся в migx-е, поэтому для них предусмотрена обработка и вывод сразу с иконками и ссылками.
Выводятся 2 списка контактов: [[+set.contacts.header]] и [[+set.contacts.all]] (если у контакта стоит галка dont_show, то он не появится в этих списках)
А также каждый контакт выводится в таком виде: [[+set.contacts.email1]] и в таком: [[+set.unique_name]] (если у контакта установлено unique_name)
Адрес почты в списках "всех контактов" и "для хедера" выводятся через сниппет botaway

Параметры:
    contactTpl - шаблон для каждого контакта (нужно указать имя чанка, либо html код с префиксом "@INLINE "),
        значение по умолчанию: \'<p class="[[+class]]"><span>[[+icon]] <strong>[[+text]]</strong> [[+value]]</span></p>\'
    settings_res_id - id ресурса, содержащего настройки
*/

class GetSiteSettings
{
    private $default_contact_tpl = \'<p class="[[+class]]"><span>[[+icon]] <strong>[[+text]]</strong> [[+value]]</span></p>\';

    public function __construct($modx, $scriptProperties)
    {
        $this->modx = $modx;
        $this->settings_res_id = $scriptProperties[\'settings_res_id\'];

        // устанавливаем шаблон контакта (из чанка, из инлайна или дефолтный)
        if (isset($scriptProperties[\'contactTpl\'])) {
            if (strpos($scriptProperties[\'contactTpl\'], \'@INLINE\') === 0) {
                $this->contact_tpl = substr($scriptProperties[\'contactTpl\'], 8);
            } else {
                $this->contact_tpl = $this->modx->getChunk($scriptProperties[\'contactTpl\']);
            }
        } else {
            $this->contact_tpl = $this->default_contact_tpl;
        }

        // получаем все "настройки сайта"
        $stmt = $this->modx->query("
            SELECT tv.name, tv_c.value
            FROM modx_site_tmplvars as tv

            LEFT JOIN modx_site_tmplvar_contentvalues as tv_c
            ON tv.id = tv_c.tmplvarid and tv_c.contentid = " . $this->settings_res_id . "
        ");

        $placeholders = array();
        while ($row = $stmt->fetch()) {
            if ($row[\'name\'] == \'contacts\') { // для тв-поля contacts - особая обработка:
                $contacts = $this->processContacts($row[\'value\']);
                $placeholders = array_merge($placeholders, $contacts);
            } else { // важно: если значение пустое, то присваиваем ему пустую строку
                $pl_name = \'set.\' . $row[\'name\'];
                $placeholders[$pl_name] = $row[\'value\'] ? $row[\'value\'] : \'\';
            }
        }
        $modx->setPlaceholders($placeholders); // установка плейсхолдеров - весь результат работы скрипта
    }

    private function fixLink($link)
    {
        if (strpos($link, \'http\') !== 0) {
            $link = \'http://\' . $link;
        }
        return $link;
    }

    private function clearPhone($input)
    {
        return preg_replace(array(\'/[^\\d]/u\', \'/^[7|8]/u\'), array(\'\', \'+7\'), $input);
    }

    private function processContacts($json_str)
    {
        $contacts_arr = json_decode($json_str);
        $result = array();
        $item_counter = array(); // счётчик для каждого из типов контактов
        foreach($contacts_arr as $contact) {
            $item_html = \'\';
            // ужасная проверка по строкам на русском языке, но иначе нельзя:
            switch($contact->contact_type) {
                case \'Телефон\':
                    $item_icon = \'<i class="fa fa-fw fa-phone"></i>\';
                    $cleared_phone = $this->clearPhone($contact->contact_value);
                    $item_value_wrapped = \'<a href="tel:\' . $cleared_phone . \'">\' . $contact->contact_value . \'</a>\';
                    $item_type = \'phone\';
                    break;
                case \'Факс\':
                    $item_icon = \'<i class="fa fa-fw fa-fax"></i>\';
                    $cleared_phone = $this->clearPhone($contact->contact_value);
                    $item_value_wrapped = \'<a href="tel:\' . $cleared_phone . \'">\' . $contact->contact_value . \'</a>\';
                    $item_type = \'fax\';
                    break;
                case \'E-mail\':
                    $item_icon = \'<i class="fa fa-fw fa-envelope-o"></i>\';
                    $item_value_wrapped = $this->modx->runSnippet(\'botaway\', array(\'email\' => $contact->contact_value));
                    //$item_value_wrapped = \'<a href="mailto:\' . $contact->contact_value . \'">\' . $contact->contact_value . \'</a>\';
                    $item_type = \'email\';
                    break;
                case \'Адрес\':
                    $item_icon = \'<i class="fa fa-fw fa-map-marker"></i>\';
                    $item_value_wrapped = $contact->contact_value;
                    $item_type = \'address\';
                    break;
                case \'Skype\':
                    $item_icon = \'<i class="fa fa-fw fa-skype"></i>\';
                    $item_value_wrapped = $contact->contact_value;
                    $item_type = \'skype\';
                    break;
                case \'ВКонтакте\':
                    $item_icon = \'<i class="fa fa-fw fa-vk"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">ВКонтакте</a>\';
                    $item_type = \'vk\';
                    break;
                case \'Facebook\':
                    $item_icon = \'<i class="fa fa-fw fa-facebook-official"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">Facebook</a>\';
                    $item_type = \'facebook\';
                    break;
                case \'Instagram\':
                    $item_icon = \'<i class="fa fa-fw fa-instagram"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">Instagram</a>\';
                    $item_type = \'instagram\';
                    break;
                case \'Веб-сайт\':
                    $item_icon = \'<i class="fa fa-fw fa-external-link"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">\' . $link . \'</a>\';
                    $item_type = \'website\';
                    break;
                case \'Время\':
                    $item_icon = \'<i class="fa fa-fw fa-clock-o" aria-hidden="true"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">\' . $link . \'</a>\';
                    $item_type = \'time\';
                    break;
                case \'Текст\':
                    $item_icon = \'\';
                    $item_value_wrapped = $contact->contact_value;
                    $item_type = \'text\';
                    break;
            }

            $item_html = str_replace(
                array(\'[[+icon]]\', \'[[+value]]\', \'[[+text]]\', \'[[+class]]\'),
                array($item_icon, $item_value_wrapped, $contact->contact_text, $contact->css_class),
                $this->contact_tpl
            );

            // значения contacts.all и contacts.header
            if ($contact->contact_value && !$contact->dont_show) {
                $result[\'set.contacts.all\'] .= $item_html;
                if ($contact->display_at_header) {
                    $result[\'set.contacts.header\'] .= $item_html;
                }
            }

            // значения плейсхолдеров со счётчиками
            $item_counter[$item_type]++;
            $item_placeholder = \'set.contacts.\' . $item_type . $item_counter[$item_type];
            $result[$item_placeholder] = $contact->contact_value;
            // доптекст поля
            $item_text_placeholder = \'set.contacts.\' . $item_type . $item_counter[$item_type] . \'.text\';
            $result[$item_text_placeholder] = $contact->contact_text;

            // выкидываем лишние символы из уникального имени
            $contact->unique_name = preg_replace(\'/[^\\w-]/\', \'\', $contact->unique_name);
            // добавляем уникальное имя контакта (если оно заполнено)
            if ($contact->unique_name) {
                $result[\'set.\' . $contact->unique_name] = $contact->contact_value;
                $result[\'set.\' . $contact->unique_name . \'.text\'] = $contact->contact_text;
            }

            //
        }
        return $result;
    }
}

new GetSiteSettings($modx, $scriptProperties);',
          'locked' => false,
          'properties' => 
          array (
            'settings_res_id' => 
            array (
              'name' => 'settings_res_id',
              'desc' => '',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '17',
              'lexicon' => NULL,
              'area' => '',
              'desc_trans' => '',
              'area_trans' => '',
            ),
          ),
          'moduleguid' => '',
          'static' => false,
          'static_file' => '',
          'content' => '/*
getSiteSettings ver: 1.9
Igor Siluyanov, 16.11.2016

Скрипт получает все значения доп. полей ресурса, содержащего настройки, и создаёт одноимённые плейсхолдеры с префиксом "set.", пример:
[[+set.site-slogan]]
Контакты хранятся в migx-е, поэтому для них предусмотрена обработка и вывод сразу с иконками и ссылками.
Выводятся 2 списка контактов: [[+set.contacts.header]] и [[+set.contacts.all]] (если у контакта стоит галка dont_show, то он не появится в этих списках)
А также каждый контакт выводится в таком виде: [[+set.contacts.email1]] и в таком: [[+set.unique_name]] (если у контакта установлено unique_name)
Адрес почты в списках "всех контактов" и "для хедера" выводятся через сниппет botaway

Параметры:
    contactTpl - шаблон для каждого контакта (нужно указать имя чанка, либо html код с префиксом "@INLINE "),
        значение по умолчанию: \'<p class="[[+class]]"><span>[[+icon]] <strong>[[+text]]</strong> [[+value]]</span></p>\'
    settings_res_id - id ресурса, содержащего настройки
*/

class GetSiteSettings
{
    private $default_contact_tpl = \'<p class="[[+class]]"><span>[[+icon]] <strong>[[+text]]</strong> [[+value]]</span></p>\';

    public function __construct($modx, $scriptProperties)
    {
        $this->modx = $modx;
        $this->settings_res_id = $scriptProperties[\'settings_res_id\'];

        // устанавливаем шаблон контакта (из чанка, из инлайна или дефолтный)
        if (isset($scriptProperties[\'contactTpl\'])) {
            if (strpos($scriptProperties[\'contactTpl\'], \'@INLINE\') === 0) {
                $this->contact_tpl = substr($scriptProperties[\'contactTpl\'], 8);
            } else {
                $this->contact_tpl = $this->modx->getChunk($scriptProperties[\'contactTpl\']);
            }
        } else {
            $this->contact_tpl = $this->default_contact_tpl;
        }

        // получаем все "настройки сайта"
        $stmt = $this->modx->query("
            SELECT tv.name, tv_c.value
            FROM modx_site_tmplvars as tv

            LEFT JOIN modx_site_tmplvar_contentvalues as tv_c
            ON tv.id = tv_c.tmplvarid and tv_c.contentid = " . $this->settings_res_id . "
        ");

        $placeholders = array();
        while ($row = $stmt->fetch()) {
            if ($row[\'name\'] == \'contacts\') { // для тв-поля contacts - особая обработка:
                $contacts = $this->processContacts($row[\'value\']);
                $placeholders = array_merge($placeholders, $contacts);
            } else { // важно: если значение пустое, то присваиваем ему пустую строку
                $pl_name = \'set.\' . $row[\'name\'];
                $placeholders[$pl_name] = $row[\'value\'] ? $row[\'value\'] : \'\';
            }
        }
        $modx->setPlaceholders($placeholders); // установка плейсхолдеров - весь результат работы скрипта
    }

    private function fixLink($link)
    {
        if (strpos($link, \'http\') !== 0) {
            $link = \'http://\' . $link;
        }
        return $link;
    }

    private function clearPhone($input)
    {
        return preg_replace(array(\'/[^\\d]/u\', \'/^[7|8]/u\'), array(\'\', \'+7\'), $input);
    }

    private function processContacts($json_str)
    {
        $contacts_arr = json_decode($json_str);
        $result = array();
        $item_counter = array(); // счётчик для каждого из типов контактов
        foreach($contacts_arr as $contact) {
            $item_html = \'\';
            // ужасная проверка по строкам на русском языке, но иначе нельзя:
            switch($contact->contact_type) {
                case \'Телефон\':
                    $item_icon = \'<i class="fa fa-fw fa-phone"></i>\';
                    $cleared_phone = $this->clearPhone($contact->contact_value);
                    $item_value_wrapped = \'<a href="tel:\' . $cleared_phone . \'">\' . $contact->contact_value . \'</a>\';
                    $item_type = \'phone\';
                    break;
                case \'Факс\':
                    $item_icon = \'<i class="fa fa-fw fa-fax"></i>\';
                    $cleared_phone = $this->clearPhone($contact->contact_value);
                    $item_value_wrapped = \'<a href="tel:\' . $cleared_phone . \'">\' . $contact->contact_value . \'</a>\';
                    $item_type = \'fax\';
                    break;
                case \'E-mail\':
                    $item_icon = \'<i class="fa fa-fw fa-envelope-o"></i>\';
                    $item_value_wrapped = $this->modx->runSnippet(\'botaway\', array(\'email\' => $contact->contact_value));
                    //$item_value_wrapped = \'<a href="mailto:\' . $contact->contact_value . \'">\' . $contact->contact_value . \'</a>\';
                    $item_type = \'email\';
                    break;
                case \'Адрес\':
                    $item_icon = \'<i class="fa fa-fw fa-map-marker"></i>\';
                    $item_value_wrapped = $contact->contact_value;
                    $item_type = \'address\';
                    break;
                case \'Skype\':
                    $item_icon = \'<i class="fa fa-fw fa-skype"></i>\';
                    $item_value_wrapped = $contact->contact_value;
                    $item_type = \'skype\';
                    break;
                case \'ВКонтакте\':
                    $item_icon = \'<i class="fa fa-fw fa-vk"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">ВКонтакте</a>\';
                    $item_type = \'vk\';
                    break;
                case \'Facebook\':
                    $item_icon = \'<i class="fa fa-fw fa-facebook-official"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">Facebook</a>\';
                    $item_type = \'facebook\';
                    break;
                case \'Instagram\':
                    $item_icon = \'<i class="fa fa-fw fa-instagram"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">Instagram</a>\';
                    $item_type = \'instagram\';
                    break;
                case \'Веб-сайт\':
                    $item_icon = \'<i class="fa fa-fw fa-external-link"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">\' . $link . \'</a>\';
                    $item_type = \'website\';
                    break;
                case \'Время\':
                    $item_icon = \'<i class="fa fa-fw fa-clock-o" aria-hidden="true"></i>\';
                    $link = $this->fixLink($contact->contact_value);
                    $item_value_wrapped = \'<a target="_blank" href="\' . $link . \'">\' . $link . \'</a>\';
                    $item_type = \'time\';
                    break;
                case \'Текст\':
                    $item_icon = \'\';
                    $item_value_wrapped = $contact->contact_value;
                    $item_type = \'text\';
                    break;
            }

            $item_html = str_replace(
                array(\'[[+icon]]\', \'[[+value]]\', \'[[+text]]\', \'[[+class]]\'),
                array($item_icon, $item_value_wrapped, $contact->contact_text, $contact->css_class),
                $this->contact_tpl
            );

            // значения contacts.all и contacts.header
            if ($contact->contact_value && !$contact->dont_show) {
                $result[\'set.contacts.all\'] .= $item_html;
                if ($contact->display_at_header) {
                    $result[\'set.contacts.header\'] .= $item_html;
                }
            }

            // значения плейсхолдеров со счётчиками
            $item_counter[$item_type]++;
            $item_placeholder = \'set.contacts.\' . $item_type . $item_counter[$item_type];
            $result[$item_placeholder] = $contact->contact_value;
            // доптекст поля
            $item_text_placeholder = \'set.contacts.\' . $item_type . $item_counter[$item_type] . \'.text\';
            $result[$item_text_placeholder] = $contact->contact_text;

            // выкидываем лишние символы из уникального имени
            $contact->unique_name = preg_replace(\'/[^\\w-]/\', \'\', $contact->unique_name);
            // добавляем уникальное имя контакта (если оно заполнено)
            if ($contact->unique_name) {
                $result[\'set.\' . $contact->unique_name] = $contact->contact_value;
                $result[\'set.\' . $contact->unique_name . \'.text\'] = $contact->contact_text;
            }

            //
        }
        return $result;
    }
}

new GetSiteSettings($modx, $scriptProperties);',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'botaway' => 
      array (
        'fields' => 
        array (
          'id' => 67,
          'source' => 2,
          'property_preprocess' => false,
          'name' => 'botaway',
          'description' => '',
          'editor_type' => 0,
          'category' => 5,
          'cache_type' => 0,
          'snippet' => '

// v. fork-0.1 (17.11.2016)

if (!empty($options)) { 
    parse_str($options); 
} 
$eml = empty($email) ? $input : $email; 
$eml = trim($eml);
$debug = !empty($debug); 
$aslink = !(isset($aslink) && $aslink == 0); 

if (filter_var($eml, FILTER_VALIDATE_EMAIL) === false) { 
    return $debug ? \'incorrect email address\' : $eml; 
} 
if (!function_exists(\'str2hex\')) { 
    function str2hex($string) 
    { 
        $hex = ""; 
        for ($i = 0; $i < strlen($string); $i++) { 
            $hex .= (strlen(dechex(ord($string[$i]))) < 2) ? "%0" . dechex(ord($string[$i])) : \'%\' . dechex(ord($string[$i])); 
        } 
        return $hex; 
    } 
} 

$enc_email = $aslink ? str2hex(\'<a href="mailto:\' . $eml . \'" rel="nofollow">\' . $eml . \'</a>\') : str2hex($eml); 
$output = \' 
    <noindex> 
        <script type="text/javascript" language="javascript"> 
            document.write(unescape(\\\'\' . $enc_email . \'\\\')); 
        </script> 
    </noindex>
\'; 
unset($input, $eml, $enc_email); 
return $output;',
          'locked' => false,
          'properties' => 
          array (
          ),
          'moduleguid' => '',
          'static' => false,
          'static_file' => '',
          'content' => '

// v. fork-0.1 (17.11.2016)

if (!empty($options)) { 
    parse_str($options); 
} 
$eml = empty($email) ? $input : $email; 
$eml = trim($eml);
$debug = !empty($debug); 
$aslink = !(isset($aslink) && $aslink == 0); 

if (filter_var($eml, FILTER_VALIDATE_EMAIL) === false) { 
    return $debug ? \'incorrect email address\' : $eml; 
} 
if (!function_exists(\'str2hex\')) { 
    function str2hex($string) 
    { 
        $hex = ""; 
        for ($i = 0; $i < strlen($string); $i++) { 
            $hex .= (strlen(dechex(ord($string[$i]))) < 2) ? "%0" . dechex(ord($string[$i])) : \'%\' . dechex(ord($string[$i])); 
        } 
        return $hex; 
    } 
} 

$enc_email = $aslink ? str2hex(\'<a href="mailto:\' . $eml . \'" rel="nofollow">\' . $eml . \'</a>\') : str2hex($eml); 
$output = \' 
    <noindex> 
        <script type="text/javascript" language="javascript"> 
            document.write(unescape(\\\'\' . $enc_email . \'\\\')); 
        </script> 
    </noindex>
\'; 
unset($input, $eml, $enc_email); 
return $output;',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'AjaxForm' => 
      array (
        'fields' => 
        array (
          'id' => 34,
          'source' => 1,
          'property_preprocess' => false,
          'name' => 'AjaxForm',
          'description' => '',
          'editor_type' => 0,
          'category' => 17,
          'cache_type' => 0,
          'snippet' => '/** @var array $scriptProperties */
/** @var AjaxForm $AjaxForm */
if (!$modx->loadClass(\'ajaxform\', MODX_CORE_PATH . \'components/ajaxform/model/ajaxform/\', false, true)) {
    return false;
}
$AjaxForm = new AjaxForm($modx, $scriptProperties);

$snippet = $modx->getOption(\'snippet\', $scriptProperties, \'FormIt\', true);
$tpl = $modx->getOption(\'form\', $scriptProperties, \'tpl.AjaxForm.example\', true);
$formSelector = $modx->getOption(\'formSelector\', $scriptProperties, \'ajax_form\', true);
$objectName = $modx->getOption(\'objectName\', $scriptProperties, \'AjaxForm\', true);
$AjaxForm->loadJsCss($objectName);

/** @var pdoTools $pdo */
if (class_exists(\'pdoTools\') && $pdo = $modx->getService(\'pdoTools\')) {
    $content = $pdo->getChunk($tpl, $scriptProperties);
} else {
    $content = $modx->getChunk($tpl, $scriptProperties);
}
if (empty($content)) {
    return $modx->lexicon(\'af_err_chunk_nf\', array(\'name\' => $tpl));
}

// Add selector to tag form
if (preg_match(\'#<form.*?class=(?:"|\\\')(.*?)(?:"|\\\')#i\', $content, $matches)) {
    $classes = explode(\' \', $matches[1]);
    if (!in_array($formSelector, $classes)) {
        $classes[] = $formSelector;
        $classes = preg_replace(
            \'#class=(?:"|\\\')\' . $matches[1] . \'(?:"|\\\')#i\',
            \'class="\' . implode(\' \', $classes) . \'"\',
            $matches[0]
        );
        $content = str_ireplace($matches[0], $classes, $content);
    }
} else {
    $content = str_ireplace(\'<form\', \'<form class="\' . $formSelector . \'"\', $content);
}

// Add method = post
if (preg_match(\'#<form.*?method=(?:"|\\\')(.*?)(?:"|\\\')#i\', $content)) {
    $content = preg_replace(\'#<form(.*?)method=(?:"|\\\')(.*?)(?:"|\\\')#i\', \'<form\\\\1method="post"\', $content);
} else {
    $content = str_ireplace(\'<form\', \'<form method="post"\', $content);
}

// Add action for form processing
$hash = md5(http_build_query($scriptProperties));
$action = \'<input type="hidden" name="af_action" value="\' . $hash . \'" />\';
if ((stripos($content, \'</form>\') !== false)) {
    if (preg_match(\'#<input.*?name=(?:"|\\\')af_action(?:"|\\\').*?>#i\', $content, $matches)) {
        $content = str_ireplace($matches[0], \'\', $content);
    }
    $content = str_ireplace(\'</form>\', "\\n\\t$action\\n</form>", $content);
}

// Save settings to user`s session
$_SESSION[\'AjaxForm\'][$hash] = $scriptProperties;

// Call snippet for preparation of form
$action = !empty($_REQUEST[\'af_action\'])
    ? $_REQUEST[\'af_action\']
    : $hash;

$AjaxForm->process($action, $_REQUEST);

// Return chunk
return $content;',
          'locked' => false,
          'properties' => 
          array (
            'actionUrl' => 
            array (
              'name' => 'actionUrl',
              'desc' => 'ajaxform_prop_actionUrl',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '[[+assetsUrl]]action.php',
              'lexicon' => 'ajaxform:properties',
              'area' => '',
              'desc_trans' => 'Коннектор для обработки ajax запросов.',
              'area_trans' => '',
            ),
            'form' => 
            array (
              'name' => 'form',
              'desc' => 'ajaxform_prop_form',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'tpl.AjaxForm.example',
              'lexicon' => 'ajaxform:properties',
              'area' => '',
              'desc_trans' => 'Чанк с формой для отправки.',
              'area_trans' => '',
            ),
            'formSelector' => 
            array (
              'name' => 'formSelector',
              'desc' => 'ajaxform_prop_formSelector',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'ajax_form',
              'lexicon' => 'ajaxform:properties',
              'area' => '',
              'desc_trans' => 'Имя CSS класса, который будеи использован как jQuery селектор для инициализации формы. По умолчанию "ajax_form".',
              'area_trans' => '',
            ),
            'frontend_css' => 
            array (
              'name' => 'frontend_css',
              'desc' => 'ajaxform_prop_frontend_css',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '/assets/extra/ajax-form/default.css',
              'lexicon' => 'ajaxform:properties',
              'area' => '',
              'desc_trans' => 'Файл с css стилями для подключения на фронтенде.',
              'area_trans' => '',
            ),
            'frontend_js' => 
            array (
              'name' => 'frontend_js',
              'desc' => 'ajaxform_prop_frontend_js',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => '/assets/extra/ajax-form/default.js',
              'lexicon' => 'ajaxform:properties',
              'area' => '',
              'desc_trans' => 'Файл с javascript для подключения на фронтенде.',
              'area_trans' => '',
            ),
            'objectName' => 
            array (
              'name' => 'objectName',
              'desc' => 'ajaxform_prop_objectName',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'AjaxForm',
              'lexicon' => 'ajaxform:properties',
              'area' => '',
              'desc_trans' => 'Имя объекта для инициализации в подключаемом javascript. По умолчанию "AjaxForm".',
              'area_trans' => '',
            ),
            'snippet' => 
            array (
              'name' => 'snippet',
              'desc' => 'ajaxform_prop_snippet',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'FormIt',
              'lexicon' => 'ajaxform:properties',
              'area' => '',
              'desc_trans' => 'Сниппет, который будет обрабатывать указанную форму.',
              'area_trans' => '',
            ),
          ),
          'moduleguid' => '',
          'static' => false,
          'static_file' => 'core/components/ajaxform/elements/snippets/snippet.ajaxform.php',
          'content' => '/** @var array $scriptProperties */
/** @var AjaxForm $AjaxForm */
if (!$modx->loadClass(\'ajaxform\', MODX_CORE_PATH . \'components/ajaxform/model/ajaxform/\', false, true)) {
    return false;
}
$AjaxForm = new AjaxForm($modx, $scriptProperties);

$snippet = $modx->getOption(\'snippet\', $scriptProperties, \'FormIt\', true);
$tpl = $modx->getOption(\'form\', $scriptProperties, \'tpl.AjaxForm.example\', true);
$formSelector = $modx->getOption(\'formSelector\', $scriptProperties, \'ajax_form\', true);
$objectName = $modx->getOption(\'objectName\', $scriptProperties, \'AjaxForm\', true);
$AjaxForm->loadJsCss($objectName);

/** @var pdoTools $pdo */
if (class_exists(\'pdoTools\') && $pdo = $modx->getService(\'pdoTools\')) {
    $content = $pdo->getChunk($tpl, $scriptProperties);
} else {
    $content = $modx->getChunk($tpl, $scriptProperties);
}
if (empty($content)) {
    return $modx->lexicon(\'af_err_chunk_nf\', array(\'name\' => $tpl));
}

// Add selector to tag form
if (preg_match(\'#<form.*?class=(?:"|\\\')(.*?)(?:"|\\\')#i\', $content, $matches)) {
    $classes = explode(\' \', $matches[1]);
    if (!in_array($formSelector, $classes)) {
        $classes[] = $formSelector;
        $classes = preg_replace(
            \'#class=(?:"|\\\')\' . $matches[1] . \'(?:"|\\\')#i\',
            \'class="\' . implode(\' \', $classes) . \'"\',
            $matches[0]
        );
        $content = str_ireplace($matches[0], $classes, $content);
    }
} else {
    $content = str_ireplace(\'<form\', \'<form class="\' . $formSelector . \'"\', $content);
}

// Add method = post
if (preg_match(\'#<form.*?method=(?:"|\\\')(.*?)(?:"|\\\')#i\', $content)) {
    $content = preg_replace(\'#<form(.*?)method=(?:"|\\\')(.*?)(?:"|\\\')#i\', \'<form\\\\1method="post"\', $content);
} else {
    $content = str_ireplace(\'<form\', \'<form method="post"\', $content);
}

// Add action for form processing
$hash = md5(http_build_query($scriptProperties));
$action = \'<input type="hidden" name="af_action" value="\' . $hash . \'" />\';
if ((stripos($content, \'</form>\') !== false)) {
    if (preg_match(\'#<input.*?name=(?:"|\\\')af_action(?:"|\\\').*?>#i\', $content, $matches)) {
        $content = str_ireplace($matches[0], \'\', $content);
    }
    $content = str_ireplace(\'</form>\', "\\n\\t$action\\n</form>", $content);
}

// Save settings to user`s session
$_SESSION[\'AjaxForm\'][$hash] = $scriptProperties;

// Call snippet for preparation of form
$action = !empty($_REQUEST[\'af_action\'])
    ? $_REQUEST[\'af_action\']
    : $hash;

$AjaxForm->process($action, $_REQUEST);

// Return chunk
return $content;',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 1,
          'name' => 'Filesystem',
          'description' => '',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
          ),
          'is_stream' => true,
        ),
      ),
    ),
    'modTemplateVar' => 
    array (
      'meta_title' => 
      array (
        'fields' => 
        array (
          'id' => 3,
          'source' => 2,
          'property_preprocess' => false,
          'type' => 'text',
          'name' => 'meta_title',
          'caption' => 'Мета заголовок страницы',
          'description' => '',
          'editor_type' => 0,
          'category' => 10,
          'locked' => false,
          'elements' => '',
          'rank' => 0,
          'display' => 'default',
          'default_text' => '',
          'properties' => 
          array (
          ),
          'input_properties' => 
          array (
            'allowBlank' => 'true',
            'maxLength' => '',
            'minLength' => '',
          ),
          'output_properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
      'keywords' => 
      array (
        'fields' => 
        array (
          'id' => 2,
          'source' => 2,
          'property_preprocess' => false,
          'type' => 'text',
          'name' => 'keywords',
          'caption' => 'Ключевые слова',
          'description' => 'Используются для SEO',
          'editor_type' => 0,
          'category' => 10,
          'locked' => false,
          'elements' => '',
          'rank' => 0,
          'display' => 'default',
          'default_text' => '',
          'properties' => 
          array (
          ),
          'input_properties' => 
          array (
            'allowBlank' => 'true',
            'maxLength' => '',
            'minLength' => '',
          ),
          'output_properties' => 
          array (
          ),
          'static' => false,
          'static_file' => '',
          'content' => '',
        ),
        'policies' => 
        array (
          'web' => 
          array (
          ),
        ),
        'source' => 
        array (
          'id' => 2,
          'name' => 'Userfiles',
          'description' => 'Пользовательские файлы',
          'class_key' => 'sources.modFileMediaSource',
          'properties' => 
          array (
            'basePath' => 
            array (
              'name' => 'basePath',
              'desc' => 'prop_file.basePath_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
            'baseUrl' => 
            array (
              'name' => 'baseUrl',
              'desc' => 'prop_file.baseUrl_desc',
              'type' => 'textfield',
              'options' => 
              array (
              ),
              'value' => 'userfiles/',
              'lexicon' => 'core:source',
            ),
          ),
          'is_stream' => true,
        ),
      ),
    ),
  ),
);