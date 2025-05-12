// Toggle show/hide for blue box text
const masterKeyBox = document.getElementById('masterKeyBox');
const toggleBtn = document.getElementById('toggleBtn');
let originalText = masterKeyBox.textContent;
let hidden = false;

toggleBtn.addEventListener('click', function() {
  if (!hidden) {
    masterKeyBox.textContent = '******';
    toggleBtn.textContent = 'Mutat';
    hidden = true;
  } else {
    masterKeyBox.textContent = originalText;
    toggleBtn.textContent = 'Elrejt';
    hidden = false;
  }
});

const playerNames = []


const transactionTypeSelector = document.getElementById("transactionType")
transactionTypeSelector.addEventListener("change", transactionTypeChanged)

const fromSelector = document.getElementById("fromPlayer")
const toSelector = document.getElementById("toPlayer")
fromSelector.addEventListener("change", removeDuplicateNames)

const createTransactionBtn = document.getElementById("createTransactionBtn")
createTransactionBtn.addEventListener("click", createTransaction)

class TransactionCreateData
{
    constructor(key, type, amount, from, to)
    {
        this.gameKey = key
        this.type = type
        this.amount = amount
        this.from = from
        this.to = to
    }
}

function createTransaction(event)
{
    event.preventDefault()
    const moneyInput = document.getElementById("moneyInput")

    const key = localStorage.getItem("key")
    const type = transactionTypeSelector.selectedIndex + 1
    const amount = moneyInput.value
    const from = fromSelector.value
    const to = toSelector.value

    const transactionCreateData = new TransactionCreateData(key, type, amount, from, to)
    if (type == 4)
    {
        transactionCreateData.amount = 2000000
    }
    const jsonString = JSON.stringify(transactionCreateData)
    // console.log(transactionCreateData)
    

    fetch("http://localhost:5078/transaction", {
        method: "POST",
        headers: {
            "Content-Type": "application/json; charset=UTF-8"
        },
        body: jsonString
    })
    .then(response => response.json())
    .then(responseData => {
        // console.log(responseData);
        
        reset()
    })
    .catch(error => {
        console.error("Error: " + error)
    })
}

function transactionTypeChanged()
{
    const selectedIndex = transactionTypeSelector.selectedIndex + 1
    const fromSelectorLabel = document.getElementById("fromPlayerLabel")
    const toSelectorLabel = document.getElementById("toPlayerLabel")
    const moneyInputBox = document.getElementById("moneyInput")
    const moneyInputLabel = document.getElementById("moneyInputLabel")

    moneyInputBox.classList.remove("d-none")
    moneyInputLabel.classList.remove("d-none")
    
    if (selectedIndex == 1)
    {
        // Utalás, kell mindkét mező
        fromSelector.classList.remove("d-none")
        fromSelectorLabel.classList.remove("d-none")
        toSelector.classList.remove("d-none")
        toSelectorLabel.classList.remove("d-none")
        removeDuplicateNames()
    }
    else if (selectedIndex == 2 || selectedIndex == 4)
    {
        // Pénz adás vagy start, csak a to mező kell
        fromSelector.classList.add("d-none")
        fromSelectorLabel.classList.add("d-none")
        toSelector.classList.remove("d-none")
        toSelectorLabel.classList.remove("d-none")
        while (toSelector.firstChild)
        {
            toSelector.removeChild(toSelector.firstChild)
        }
        for (let i = 0; i < playerNames.length; i++)
        {
            toSelector.innerHTML = toSelector.innerHTML + "<option>" + playerNames[i] + "</option>"
        }

        if (selectedIndex == 4)
        {
            moneyInputBox.classList.add("d-none")
            moneyInputLabel.classList.add("d-none")
        }
    }
    else if (selectedIndex == 3)
    {
        // Pénz elvétel, csak a from mező kell
        fromSelector.classList.remove("d-none")
        fromSelectorLabel.classList.remove("d-none")
        toSelector.classList.add("d-none")
        toSelectorLabel.classList.add("d-none")
    }
    
}

function removeDuplicateNames()
{
    // const selectedName = fromSelector.value

    // while (toSelector.firstChild)
    // {
    //     toSelector.removeChild(toSelector.firstChild)
    // }
    // for (let i = 0; i < playerNames.length; i++)
    // {
    //     if (playerNames[i] == selectedName)
    //     {
    //         continue
    //     }
    //     toSelector.innerHTML = toSelector.innerHTML + "<option>" + playerNames[i] + "</option>"
    // }
}




const keyObj = new Object()
keyObj.key = localStorage.getItem("key")
keyObj.type = localStorage.getItem("keyType")

function reset()
{
    
    fetch(`http://localhost:5078/game/${keyObj.key}`)
        .then(response => response.json())
        .then(data => {
            // console.log(data);
            const viewKeyBox = document.getElementById("viewKeyBox")
            
            masterKeyBox.innerHTML = data.key
            originalText = data.key
            viewKeyBox.innerHTML = data.viewKey

            const fromPlayerSelect = document.getElementById("fromPlayer")
            fromPlayerSelect.innerHTML = ""
            const toPlayerSelect = document.getElementById("toPlayer")
            toPlayerSelect.innerHTML = ""

            if (localStorage.getItem("keyType") == "view")
            {
                const createTransactionHolder = document.getElementById("createTransaction")
                createTransactionHolder.classList.add("d-none")
            }

            // Playerek beállítása a selectbe
            let options = ""
            for (let i = 0; i < data.players.length; i++)
            {
                options = options + "<option>" + data.players[i].name + "</option>"
            }
            fromPlayerSelect.innerHTML = options
            toPlayerSelect.innerHTML = options

            // tranzakciók beállítása
            const transactionHolder = document.getElementById("transactionsHolder")
            while (transactionHolder.firstChild)
            {
                transactionHolder.removeChild(transactionHolder.firstChild)
            }

            for (let i = data.transactions.length - 1; i >= 0; i--)
            {
                currentTransaction = data.transactions[i]
                if (currentTransaction.result == 1)
                {
                    continue
                }
                const type = currentTransaction.type
                const to = currentTransaction.to
                const from = currentTransaction.from
                const money = currentTransaction.amount
                transactionHolder.appendChild(transactionCardGenerator(type, to, from, money))
            }

            // Játékosok és pénzeik beállítása
            const playerHolder = document.getElementById("playerHolder")
            while (playerHolder.firstChild)
            {
                playerHolder.removeChild(playerHolder.firstChild)
            }

            for (let i = 0; i < data.players.length; i++)
            {
                currentPlayer = data.players[i]
                const name = currentPlayer.name
                const money = currentPlayer.money
                playerHolder.appendChild(playerCardGenerator(name, money))
                playerNames.push(name)
            }

            removeDuplicateNames()
        })
        .catch(error => console.error('Error:', error))
}

function playerCardGenerator(name, money)
{
    const playerListItem = document.createElement("li")
    playerListItem.setAttribute("class", "list-group-item")
    playerListItem.classList.add("d-flex")
    playerListItem.classList.add("justify-content-between")
    playerListItem.classList.add("align-items-center")
    playerListItem.innerHTML = "<b>" + name + "</b> " + money.toLocaleString('de-DE') + "$"

    // playerMoneyPill = document.createElement("span")
    // // playerMoneyPill.setAttribute("style", "font-size: 1rem")
    // playerMoneyPill.setAttribute("class", "badge")
    // playerMoneyPill.classList.add("bg-success")
    // playerMoneyPill.classList.add("rounded-pill")
    // playerMoneyPill.innerHTML = money

    // playerListItem.appendChild(playerMoneyPill)

    return playerListItem
}

function transactionCardGenerator(type, to, from, money)
{
    const transactionCard = document.createElement("div")
    transactionCard.setAttribute("class", "card")
    transactionCard.classList.add("mb-2")

    const transactionCardInside = document.createElement("div")
    transactionCardInside.setAttribute("class", "card-body")
    transactionCardInside.classList.add("py-2")
    transactionCardInside.classList.add("px-3")

    money = money.toLocaleString('de-DE')
    let innerHtml = ""
    if (type == 1)
    {
        // Utalás
        transactionCard.classList.add("bg-warning")
        transactionCard.classList.add("text-dark")
        innerHtml = innerHtml + "<strong>Utalás:</strong> " + from + " → " + to + "<br>"
        innerHtml = innerHtml + "<small>Összeg: " + money + "</small>"
    }
    else if (type == 2)
    {
        // Pénz adás
        transactionCard.classList.add("bg-success")
        transactionCard.classList.add("text-white")
        innerHtml = innerHtml + "<strong>Pénz hozzáadás:</strong> " + to + "<br>"
        innerHtml = innerHtml + "<small>Összeg: " + money + "</small>"
    }
    else if (type == 3)
    {
        // Pénz elvétel
        transactionCard.classList.add("bg-danger")
        transactionCard.classList.add("text-white")
        innerHtml = innerHtml + "<strong>Pénz elvétel:</strong> " + from + "<br>"
        innerHtml = innerHtml + "<small>Összeg: " + money + "</small>"
    }
    else if (type == 4)
    {
        // Startmező
        transactionCard.classList.add("bg-primary")
        transactionCard.classList.add("text-white")
        innerHtml = innerHtml + "<strong>Start:</strong> " + to + "<br>"
        innerHtml = innerHtml + "<small>Összeg: " + money + "</small>"
    }
    transactionCardInside.innerHTML = innerHtml
    transactionCard.appendChild(transactionCardInside)

    return transactionCard
}

reset()


if (localStorage.getItem("keyType") == "view")
{
    setInterval(reset, 2000)
}
  