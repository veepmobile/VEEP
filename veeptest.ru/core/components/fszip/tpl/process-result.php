<?php if ($success): ?>
    <?=$success_message?>. <a href="<?=$href?>" class="js-download-link"><?=$link_text?></a>
<?php else: ?>
    <span class="error">Ошибка:</span>
    <?=$error_message?>
<?php endif; ?>
