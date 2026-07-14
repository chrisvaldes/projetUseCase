window.drawDashboardChart = function(labels, montants) {

    const canvas = document.getElementById("myChart");

    if (!canvas)
        return;

    const ctx = canvas.getContext("2d");

    new Chart(ctx,{
        type:"line",
        data:{
            labels:labels,
            datasets:[{
                label:"Montant",
                data:montants
            }]
        }
    });

}