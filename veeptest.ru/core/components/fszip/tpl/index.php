<link rel="stylesheet" href="/assets/components/fszip/css/style.css">
<script src="/assets/components/fszip/js/jquery.min.js"></script>
<script src="/assets/components/fszip/js/script.js"></script>

<div id="fszip">
    <h2>Архиватор</h2>
    <div class="group">
        <span class="x-btn primary-button js-action" data-action="make-all">Сделать дамп и архив сайта</span>
    </div>
    <div class="group">
        Ручная архивация (если не сработала автоматическая):
    </div>
    <div class="group">
        <span class="x-btn js-action" data-action="make-archive">Сделать архив сайта</span>
        <span class="x-btn js-action" data-action="make-dump">Сделать дамп базы</span>
    </div>
    <div class="js-indicator">
        Обработка...
    </div>
    <div class="group js-result"></div>
</div>
