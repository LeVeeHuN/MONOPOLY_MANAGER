const gameKeyInput = document.getElementById("gamekeyinput")
const joinGameBtn = document.getElementById("joingamebtn")
const newGameBtn = document.getElementById("newgamebtn")

joinGameBtn.addEventListener("click", joinGame)
newGameBtn.addEventListener("click", newGame)

function joinGame(event)
{
    // This way the page doesnt reload after clicking the button
    event.preventDefault()

    const gameCode = gameKeyInput.value.toUpperCase()
    
    // const params = new URLSearchParams({key: gameCode})
    fetch(`http://localhost:5078/game/${gameCode}`)
        .then(response => response.json())
        .then(data => {
            // console.log(data);
            localStorage.setItem("key", gameCode)
            if (data.key == "0")
            {
                localStorage.setItem("keyType", "view")
            }
            else
            {
                localStorage.setItem("keyType", "master")
            }
            window.location.replace('./game.html')
        })
        .catch(error => console.error('Error:', error))
}

function newGame(event)
{
    window.location.replace('./newgame.html')
}