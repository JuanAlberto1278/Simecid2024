﻿const nav = new DayPilot.Navigator("nav", {
    showMonths: 3,
    skipMonths: 3,
    selectMode: "Week",
    onTimeRangeSelected: args => {
        dp.update({
            startDate: args.day
        });
        app.loadEvents();
    }
});
nav.init();

const dp = new DayPilot.Calendar("dp", {
    viewType: "Week",
    /*
    eventDeleteHandling: "Update",
    onEventDeleted: async (args) => {
        const id = args.e.id();
        await DayPilot.Http.delete(`/api/CalendarEvents/${id}`);
        console.log("Deleted.");
    },
    */
    onEventMoved: async (args) => {
        const id = args.e.id();
        const data = {
            id: args.e.id(),
            start: args.newStart,
            end: args.newEnd,
            text: args.e.text()
        };
        await DayPilot.Http.put(`/api/CalendarEvents/${id}`, data);
        console.log("Moved.");
    },
    onEventResized: async (args) => {
        const id = args.e.id();
        const data = {
            id: args.e.id(),
            start: args.newStart,
            end: args.newEnd,
            text: args.e.text()
        };
        await DayPilot.Http.put(`/api/CalendarEvents/${id}`, data);
        console.log("Resized.");
    },
    onTimeRangeSelected: async (args) => {
        const form = [
            { name: "Name", id: "text" }
        ];

        const modal = await DayPilot.Modal.form(form, { text: "Event" });
        dp.clearSelection();

        if (modal.canceled) {
            return;
        }

        const event = {
            start: args.start,
            end: args.end,
            text: modal.result.text
        };
        const { data } = await DayPilot.Http.post(`/api/CalendarEvents`, event);

        dp.events.add({
            start: args.start,
            end: args.end,
            id: data.id,
            text: modal.result.text
        });
        console.log("Created.");

    },
    onEventClick: async (args) => {
        app.editEvent(args.e);
    },
    onBeforeEventRender: args => {
        args.data.areas = [
            {
                top: 5,
                right: 5,
                width: 16,
                height: 16,
                symbol: "icons/daypilot.svg#minichevron-down-4",
                fontColor: "#666",
                visibility: "Hover",
                action: "ContextMenu",
                style: "background-color: #f9f9f9; border: 1px solid #666; cursor:pointer; border-radius: 15px;"
            }
        ];
    },
    contextMenu: new DayPilot.Menu({
        items: [
            {
                text: "Edit...",
                onClick: args => {
                    app.editEvent(args.source);
                }
            },
            {
                text: "Delete",
                onClick: args => {
                    app.deleteEvent(args.source);
                }
            },
            {
                text: "-"
            },
            {
                text: "Duplicate",
                onClick: args => {
                    app.duplicateEvent(args.source);
                }
            },
        ]
    })
});
dp.init();


const app = {
    elements: {
        theme: document.querySelector("#theme")
    },
    loadEvents() {
        dp.events.load("/api/CalendarEvents");
    },
    async editEvent(e) {
        const form = [
            { name: "Name", id: "text" }
        ];

        const modal = await DayPilot.Modal.form(form, e.data);
        if (modal.canceled) {
            return;
        }

        const id = e.id();
        const data = {
            id: e.id(),
            start: e.start(),
            end: e.end(),
            text: modal.result.text
        };
        await DayPilot.Http.put(`/api/CalendarEvents/${id}`, data);

        dp.events.update({
            ...e.data,
            text: modal.result.text
        });
        console.log("Updated.");
    },
    async deleteEvent(e) {
        const modal = await DayPilot.Modal.confirm("Do you really want to delete this event?");
        if (modal.canceled) {
            return;
        }
        const id = e.id();
        await DayPilot.Http.delete(`/api/CalendarEvents/${id}`);

        dp.events.remove(id);

        console.log("Deleted.");
    },
    async duplicateEvent(e) {
        const event = {
            start: e.start(),
            end: e.end(),
            text: e.text() + " (copy)"
        };
        const { data } = await DayPilot.Http.post(`/api/CalendarEvents`, event);

        dp.events.add({
            ...event,
            id: data.id,
        });
        console.log("Duplicated.");
    },
    init() {
        app.elements.theme.addEventListener("change", () => {
            dp.update({
                theme: app.elements.theme.value
            });
        });

        dp.update({ theme: app.elements.theme.value });
        app.loadEvents();
    }
};
app.init();

