let arrFlights = [];

arrFlights[0] = {
    Number: "PS101",
    Start: "Київ",
    End: "Варшава",
    Duration: 90,
    Time: "12:30"
};

arrFlights[1] = {
    Number: "PS102",
    Start: "Київ",
    End: "Прага",
    Duration: 120,
    Time: "14:45"
};

arrFlights[2] = {
    Number: "PS103",
    Start: "Львів",
    End: "Берлін",
    Duration: 150,
    Time: "16:20"
};

arrFlights[3] = {
    Number: "PS104",
    Start: "Одеса",
    End: "Відень",
    Duration: 110,
    Time: "18:00"
};

arrFlights[4] = {
    Number: "PS105",
    Start: "Київ",
    End: "Париж",
    Duration: 180,
    Time: "20:15"
};

arrFlights[5] = {
    Number: "PS106",
    Start: "Львів",
    End: "Прага",
    Duration: 95,
    Time: "10:40"
};

arrFlights[6] = {
    Number: "PS107",
    Start: "Одеса",
    End: "Варшава",
    Duration: 130,
    Time: "11:15"
};

arrFlights[7] = {
    Number: "PS108",
    Start: "Київ",
    End: "Берлін",
    Duration: 140,
    Time: "13:50"
};

arrFlights[8] = {
    Number: "PS109",
    Start: "Львів",
    End: "Відень",
    Duration: 105,
    Time: "17:10"
};

arrFlights[9] = {
    Number: "PS110",
    Start: "Одеса",
    End: "Париж",
    Duration: 210,
    Time: "19:30"
};

arrFlights[10] = {
    Number: "PS111",
    Start: "Київ",
    End: "Відень",
    Duration: 115,
    Time: "21:00"
};

arrFlights[11] = {
    Number: "PS112",
    Start: "Львів",
    End: "Варшава",
    Duration: 80,
    Time: "22:10"
};

let html = "";

function timeDifference(time) {
    let currentDate = new Date();

    let currentHours = currentDate.getHours();
    let currentMinutes = currentDate.getMinutes();

    let currentTime = currentHours * 60 + currentMinutes;

    let parts = time.split(":");
    let flightTime = Number(parts[0]) * 60 + Number(parts[1]);

    let diffTime = flightTime - currentTime;

    return diffTime;
}

function output(item, i, array) {
    let diffTime = timeDifference(item.Time);

    html = html + "<tr>";

    html = html + "<td>" + item.Number + "</td>";
    html = html + "<td>" + item.Start + "</td>";
    html = html + "<td>" + item.End + "</td>";
    html = html + "<td>" + item.Duration + " хв</td>";
    html = html + "<td>" + item.Time + "</td>";

    if (diffTime > 0) {
        html = html + "<td>До вильоту " + diffTime + " хв</td>";
    }
    else if (diffTime <= 0 && Math.abs(diffTime) < item.Duration) {
        html = html + "<td>Рейс у повітрі</td>";
    }
    else {
        html = html + "<td>Рейс завершено</td>";
    }

    html = html + "</tr>";
}

function table() {
    html = "<table border='1'>";

    html = html + "<tr>";
    html = html + "<th>Номер рейсу</th>";
    html = html + "<th>Початковий пункт</th>";
    html = html + "<th>Кінцевий пункт</th>";
    html = html + "<th>Тривалість</th>";
    html = html + "<th>Час вильоту</th>";
    html = html + "<th>Статус</th>";
    html = html + "</tr>";

    let selectedStart = document.getElementById("startPoint").value;
    let selectedEnd = document.getElementById("endPoint").value;

    arrFlights.forEach(function(item, i, array) {

        if (item.Start == selectedStart && item.End == selectedEnd) {
            output(item, i, array);
        }

    });

    html = html + "</table>";

    document.getElementById("result").innerHTML = html;
}