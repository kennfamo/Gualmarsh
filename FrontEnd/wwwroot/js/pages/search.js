$(function () {
    $("#autoComplete").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Products/Index?handler=AutoComplete',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { "prefix": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#hfProduct").val(i.item.val);
        },
        minLength: 1
    });
});
/*
const autoCompleteJS = new autoComplete(
    {
        selector: "#autoComplete",
        placeHolder: "Search...",
        data: {
            src: async () => {
                try {
                    const source = await fetch(`/Products/Index?handler=AutoComplete/`);
                    const data = await source.json();
                    console.log(data)
                    return data;
                } catch (error) {
                    return error
                }
            },
            cache: true,
        },
        resultsList: {
            element: (list, data) => {
                if (!data.results.length) {
                    // Create "No Results" message element
                    const message = document.createElement("div");
                    // Add class to the created element
                    message.setAttribute("class", "no_result");
                    // Add message text content
                    message.innerHTML = `<span>Found No Results for "${data.query}"</span>`;
                    // Append message element to the results list
                    list.prepend(message);
                }
            },
            noResults: false,
        },
        resultItem: {
            highlight: true,
        },
        events: {
            input: {
                focus: () => {
                    if (autoCompleteJS.input.value.length) autoCompleteJS.start();
                }
            },
        },
    });
    autoCompleteJS.input.addEventListener("selection", function (event) {
        const feedback = event.detail;
        autoCompleteJS.input.blur();
        // Prepare User's Selected Value
        const selection = feedback.selection.value;
        console.log(selection)
        autoCompleteJS.input.value = selection;
        if (selection.startsWith("C")) {
            window.location.replace(`/cotizaciones/cotizacion/${selection}`)
        } else {
            window.location.replace(`/tramite/${selection}`)
        }

    });
*/