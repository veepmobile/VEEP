<?php
$iPod    = stripos($_SERVER['HTTP_USER_AGENT'],"iPod");
$iPhone  = stripos($_SERVER['HTTP_USER_AGENT'],"iPhone");
$iPad    = stripos($_SERVER['HTTP_USER_AGENT'],"iPad");
$Android = stripos($_SERVER['HTTP_USER_AGENT'],"Android");
$webOS   = stripos($_SERVER['HTTP_USER_AGENT'],"webOS");
$ios_link = 'https://itunes.apple.com/ru/app/veep/id1109320099?mt=8';
$android_link = 'https://play.google.com/store/apps/details?id=ru.veeppay.app';
if ($iPod || $iPhone || $iPad) {
	header('Location: '.$ios_link);
} else if ($Android) {
	header('Location: '.$android_link);
} else {
?>
<!doctype html>
<html>
<head>
<meta charset="utf-8">
<title>Veep</title>
<meta name="description" content="">
<meta name="Keywords" content="" >
<meta property="og:type" content="website" />
<meta property="og:title" content="Veep" />
<meta property="og:description" content="VEEP - мобильное приложение, открывающее пользователю уникальные возможности при посещении кафе и ресторанов." />
<meta property="og:image" content="http://veeppay.ru/app/img/veep.png" />
<meta property="og:url" content="http://veeppay.ru/app/" />
<link href="css/style.css" rel="stylesheet" />
</head>

<body>
<div class="wrap">
	<div class="button"><img src="img/veep.png" alt="Veep"></div>
	<a class="button" href="<?php echo $ios_link?>"><img src="img/app_store.png" alt=""></a>
	<a class="button" href="<?php echo $android_link?>"><img src="img/google_play.png" alt=""></a>
</div>
</body>
</html>

<?php
}

exit();
