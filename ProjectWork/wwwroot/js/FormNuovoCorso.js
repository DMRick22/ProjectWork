function ControlliNuovoCorso() {
    let errors = [];

    if (document.getElementById('nome').value.length <= 1) {
        errors.push("Il nome del corso deve avere almeno 2 caratteri")
    }

    if (document.getElementById('autore').value.length <= 1) {
        errors.push("L'autore del corso deve avere almeno 2 caratteri")
    }

    if (document.getElementById('linguaggio').value.length <= 1) {
        errors.push("Il linguaggio del corso deve avere almeno 2 caratteri")
    }

    if (document.getElementById('descrizione').value.length >= 100) {
        errors.push("La descrizione del corso non può essere lunga più di 100 caratteri")
    }

    if (errors.length > 0) {
        alert(errors.join("\n"));
        return false;
    }

    return true;
}