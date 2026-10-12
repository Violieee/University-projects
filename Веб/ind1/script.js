groups = new Array();

async function populate() {
    const requestURL = "https://api.jsonbin.io/v3/b/6acccf9aac6210605a2a785a";
    const request = new Request(requestURL);
    const response = await fetch(request);
    if (response.ok) {
        const gr = await response.json();
        newGroup (gr.record);
        showHeader();
        showGroup();;
    }
    else
        alert("Помилка");
}

function newGroup(obj) {
    for (const g of obj) {
        let grp = new grouping(g.group, g.specialty);
        groups.push(grp);
    }
}

function showHeader() {
    const header = document.querySelector("header");

    const myH1 = document.createElement("h1");
    myH1.innerText = "Самостійна робота №1";
    const myP1 = document.createElement("p");
    myP1.innerText = "Скрипка Софія";
    const myP2 = document.createElement("p");
    myP2.innerText = "КН-31";

    header.appendChild(myH1);
    header.appendChild(myP1);
    header.appendChild(myP2);
}

function showGroup() {
    const main = document.querySelector("article");

    const divCS = document.createElement("div");
    const divSE = document.createElement("div");
    const divCSec = document.createElement("div");

    const myH2CS = document.createElement("h2");
    const myH2SE = document.createElement("h2");
    const myH2CSec = document.createElement("h2");

    myH2CS.textContent = "Комп'ютерні науки";
    myH2SE.textContent = "Інженерія програмного забезпечення";
    myH2CSec.textContent = "Кібербезпека";

    const myListCS = document.createElement("ul");
    const myListSE = document.createElement("ul");
    const myListCSec = document.createElement("ul");

    for (const g of groups) {
        const listItem = document.createElement("li");
        listItem.textContent = g.group;

        if (g.specialty == "Комп'ютерні науки") {
            myListCS.appendChild(listItem);
        }
        else if (g.specialty == "Інженерія програмного забезпечення") {
            myListSE.appendChild(listItem);
        }
        else if (g.specialty == "Кібербезпека") {
            myListCSec.appendChild(listItem);
        }
    }

    divCS.appendChild(myH2CS);
    divCS.appendChild(myListCS);

    divSE.appendChild(myH2SE);
    divSE.appendChild(myListSE);

    divCSec.appendChild(myH2CSec);
    divCSec.appendChild(myListCSec);

    main.appendChild(divCS);
    main.appendChild(divSE);
    main.appendChild(divCSec);
}

populate();
