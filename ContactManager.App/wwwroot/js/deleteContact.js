function deleteContact(contactId) {
    $.ajax({
        url: '/contact/delete/' + contactId,
        type: 'DELETE',
        dataType: 'json',
        success: function (response) {
            window.location.href = response.redirectToUrl;
        },
        error: function (error) {
            console.error('Error:', error);
            alert('Error');
        }
    });
}
