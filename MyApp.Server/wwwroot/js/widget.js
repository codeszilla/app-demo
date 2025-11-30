window.createSortableWidgets = () => {

    function initSortable() {
        const container = document.getElementById("widgetContainer");

        if (!container) {
            console.log("widgetContainer not found yet — retrying...");
            setTimeout(initSortable, 200); // retry until the DOM exists
            return;
        }

        console.log("Sortable initialized!");

        Sortable.create(container, {
            animation: 150,
            ghostClass: "sortable-ghost"
        });
    }

    // start checking
    initSortable();
};

/*
window.loadWidgetOrder = () => {
    let x = localStorage.getItem("widgetOrder");
    return x ? JSON.parse(x) : null;
};*/


window.createSortableWidgets = function () {
    const container = document.getElementById("widgetContainer");
    if (!container) {
        console.warn("widgetContainer not found");
        return;
    }

    let dragged = null;

    container.querySelectorAll(".widget-card").forEach(card => {
        card.addEventListener("dragstart", (e) => {
            dragged = card;
            e.dataTransfer.effectAllowed = "move";
            card.classList.add("dragging");
        });

        card.addEventListener("dragend", () => {
            dragged.classList.remove("dragging");
            dragged = null;

            saveOrder();
        });

        card.addEventListener("dragover", (e) => {
            e.preventDefault();

            const bounding = card.getBoundingClientRect();
            const offset = e.clientY - bounding.top - (bounding.height / 2);

            if (offset > 0) {
                card.after(dragged);
            } else {
                card.before(dragged);
            }
        });
    });

    function saveOrder() {
        const ids = [...container.querySelectorAll(".widget-card")]
            .map(x => x.dataset.id);

        localStorage.setItem("widgetOrder", JSON.stringify(ids));
    }
};

window.loadWidgetOrder = function () {
    const saved = localStorage.getItem("widgetOrder");
    return saved ? JSON.parse(saved) : null;
};
