function getCookieValue(key) {
    let ans = ""

    let cookies = document.cookie.split(";")
    cookies.forEach(cookie => {

        let cookieParts = cookie.split("=");
        if (cookieParts[0].includes(key)) {
            ans = cookieParts[1]
        }
    })

    return ans
}

function decode(data){
    let result = "";
    const separateData = data.split(".");
    for (const encodedSymbol of separateData) {
        result += String.fromCharCode(Number(encodedSymbol));
    }
    return result;
}

function getHolder() {
    let holder = getCookieValue("holder")
    if (holder.includes(".")) {
        let holderParts = holder.split("-")
        let leftPart = decode(holderParts[0])
        let rightPart = decode(holderParts[1])

        return leftPart + "-" + rightPart
    }

    return holder
}