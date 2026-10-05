let cells = document.querySelectorAll("td");

cells.forEach(function(cell) {

    cell.addEventListener("click", function() {

        let value = cell.textContent;

        if (!isNaN(value) && value.trim() != "") {

            let number = Number(value);

            if (number < 0) {
                cell.style.backgroundColor = "lightcoral";
            }
            else if (number > 0) {
                cell.style.backgroundColor = "lightgreen";
            }
            else {
                cell.style.backgroundColor = "lightyellow";
            }

        }
        else {
            cell.style.backgroundColor = "lightblue";
        }

    });

});