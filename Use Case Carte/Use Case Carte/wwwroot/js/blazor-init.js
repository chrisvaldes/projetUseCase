// Réinitialisation minimaliste — adaptez selon defaultmenu.min.js / simplebar API
window.reinitUi = function () {
    try {
        // si vous utilisez SimpleBar : recréer sur le conteneur de la sidebar
        if (window.SimpleBar) {
            const el = document.getElementById('sidebar-scroll');
            if (el) new SimpleBar(el);
        }
        // Si defaultmenu expose une fonction d'init : appelez-la ici
        if (window.DefaultMenu && typeof window.DefaultMenu.init === 'function') {
            window.DefaultMenu.init();
        }
    } catch (e) {
        console.error('reinitUi error', e);
    }
};