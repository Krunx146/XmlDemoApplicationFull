
function getBookData() {
    $.ajax({
        url: Urls.GetData,
        cache: false,
        dataType: 'json',
        success: function(data) {
            $("#dataContainer").empty();

            var username = document.getElementById("borrowName").value;

            if (Array.isArray(data)) {
                data.forEach((book) => {
                    var buttonTemplate = username.length > 0 && book.BorrowedBy == username
                        ? `<button type="button" onclick="returnBook('${book.Id}')">Vrati</button>`
                        : `<button type="button" ${book.IsBorrowed ? "disabled='disabled'" : ""} onclick="borrowBook('${book.Id}')">Posudi</button>`;
                    $("#dataContainer").append(`
                        <div class="book-item">
                            <div class="head">Knjiga</div>
                            <div class="top">
                                <div>${ book.Author }</div>
                                <div>${ book.Title }</div>
                                <div>${ book.Genre }</div>
                                <div>${ book.Price }</div>
                                <div>${ new Date(book.PublishDate).toLocaleDateString() }</div>
                            </div>
                            <div class="middle">
                                ${ book.Description }
                            </div>
                            <div class="bottom">
                                <div class="status" style="background: ${ book.IsBorrowed ? 'indianred' : 'mediumseagreen' };">
                                    ${ book.IsBorrowed ? 'POSUĐENO: ' + book.BorrowedBy : 'SLOBODNO' }
                                </div>
                                ` + buttonTemplate + `
                            </div>
                        </div>
                    `)
                });
            }
        }
    });
}

function borrowBook(bookId) {

    let username = document.getElementById("borrowName").value;
    if (username.length < 1) {
        return alert("Korisničko ime je prazno. Nije moguće posuditi knjigu.");
    }

    $.ajax({
        url: Urls.BorrowBook,
        type: 'POST',
        data: {
            bookId: bookId,
            username: username
        },
        success: function(text) {
            alert("Uspjeh! " + text);
            getBookData();
        },
        error: function(xhr) {
            alert(xhr.responseText);
        }
    });

}

function returnBook(bookId) {

    let username = document.getElementById("borrowName").value;
    if (username.length < 1) {
        return alert("Korisničko ime je prazno. Nije moguće vratiti knjigu.");
    }

    $.ajax({
        url: Urls.ReturnBook,
        type: 'POST',
        data: {
            bookId: bookId,
            username: username
        },
        success: function(text) {
            alert("Uspjeh! " + text);
            getBookData();
        },
        error: function(xhr) {
            alert(xhr.responseText);
        }
    });

}

// Doc ready
$(function() {

    getBookData();

    var refreshTimeout = null;
    document.getElementById("borrowName").addEventListener("change", function() {
        clearTimeout(refreshTimeout);
        refreshTimeout = setTimeout(getBookData, 500);
    });

});