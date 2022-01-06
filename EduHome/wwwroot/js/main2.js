$(function () {
    $(document).on("click", ".modal-btn", function (event) {
        event.preventDefault();
        console.log("Click")

        let id = $(this).attr("data-id")

        fetch("http://localhost:60244/detail" + id)
            .then(response => response.text)
            .then(data => {
                $(".modal-content").html(data)
            })

        $("#clickModal").modal("show")

    })

    $(document).on("click", ".add-btn", function (event) {
        event.preventDefault();
        console.log("Click addBnt")

        let url = $(this).attr("href");
        fetch(url)
            .then(response => {
                console.log(response)
                if (response.ok) {
                    alert("Sebete elave edildi!")
                }
                else {
                    alert("Xeta bas verdi!")
                }
            })

        console.log(url);

    })

    $(function () {
        $(document).on("click", ".img-box-remove", function () {
            console.log("img remove")
            $(".img-box").remove()
        })
    })
})