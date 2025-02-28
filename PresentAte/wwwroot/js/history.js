function filterPresentations() {
    let input = document.getElementById('searchBar').value.toLowerCase();
    let cards = document.querySelectorAll('.card');

    cards.forEach(card => {
        let title = card.getAttribute('data-title').toLowerCase();
        if (title.includes(input)) {
            card.style.display = "block";
        } else {
            card.style.display = "none";
        }
    });
}

function deletePresentation(presentationId) {
    if (confirm("Are you sure you want to delete this presentation?")) {
        fetch(`Delete/${presentationId}`, { method: 'DELETE' })
            .then(response => {
                if (response.ok) {
                    location.reload(); // Reload the page to reflect the deletion
                } else {
                    alert("Error deleting presentation.");
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert("Error deleting presentation.");
            });
    }
}