window.initPermissionTree = (containerId, data) => {
    const container = document.getElementById(containerId);
    if (!container) {
        console.error(`Conteneur ${containerId} introuvable`);
        return;
    }

    // Détruit une instance précédente si elle existe (utile en cas de re-render Blazor)
    if ($.jstree.reference(container)) {
        $(container).jstree("destroy");
    }

    $(container).jstree({
        core: {
            data: data.map(node => ({
                id: node.id,
                parent: node.parent,
                text: `${node.text} (${node.code})`
            })),
            themes: {
                dots: true,
                icons: true
            }
        }
    });
};