const createGameBtn = document.getElementById("createGameBtn")
createGameBtn.addEventListener("click", createGame)

class GameCreationData
{
    constructor(startMoney, startTileMoney, players)
    {
        this.startMoney = Number(startMoney)
        this.startTileMoney = Number(startTileMoney)
        this.players = players
    }
}

function createGame(event)
{
    event.preventDefault()

    const startMoney = document.getElementById("startingMoney").value
    const startTileMoney = document.getElementById("startFieldMoney").value

    // Get the players
    const players = []
    const playerInputs = document.getElementsByClassName("player-name-input")
    for (let i = 0; i < playerInputs.length; i++)
    {
        if (playerInputs[i].value.length > 0)
        {
            players.push(playerInputs[i].value)
        }
    }
    
    const gameCreationData = new GameCreationData(startMoney, startTileMoney, players)

    // send request to the server
    const jsonString = JSON.stringify(gameCreationData)

    fetch("http://localhost:5078/game", {
        method: "POST",
        headers: {
            "Content-Type": "application/json; charset=UTF-8"
        },
        body: jsonString
    })
    .then(response => response.json())
    .then(responseData => {
        // console.log(responseData)
        localStorage.setItem("key", responseData.key)
        localStorage.setItem("keyType", "master")
        window.location.replace("./game.html")
    })
    .catch(error => {
        console.error("Error: " + error)
    })

}