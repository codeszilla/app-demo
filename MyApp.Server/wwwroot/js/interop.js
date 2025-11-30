
window.setCookie = function (name, value, hours) {
    const d = new Date();
    d.setTime(d.getTime() + (hours * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = name + "=" + value + ";" + expires + ";path=/;SameSite=None;Secure";
}

window.getCookie = function (name) {
    const decoded = decodeURIComponent(document.cookie);
    const parts = decoded.split('; ');
    for (let part of parts) {
        if (part.startsWith(name + "=")) {
            return part.substring(name.length + 1);
        }
    }
    return "";
}

window.deleteCookie = function (name) {
    const domains = [
        "",
        "." + window.location.hostname,
        window.location.hostname
    ];

    const paths = ["/", window.location.pathname];

    domains.forEach(domain => {
        paths.forEach(path => {
            document.cookie =
                `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=${path}; domain=${domain}; Secure; SameSite=None`;
        });
    });
};

window.showConfirm = (title, text) => {
    return Swal.fire({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        return result.isConfirmed; // true if "Yes" clicked
    });
};

window.showModal = (modalId) => {
    var modalElement = document.getElementById(modalId);
    var modal = new bootstrap.Modal(modalElement);
    modal.show();
};

window.hideModal = (modalId) => {
    var modalElement = document.getElementById(modalId);
    var modal = bootstrap.Modal.getInstance(modalElement);
    modal.hide();
};
