window.toastSuccess = (message) => {
    Swal.fire({
        toast: true,
        position: 'top-center',
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: 2000
    });
};

window.toastWarning = (message) => {
    Swal.fire({
        toast: true,
        position: 'top-center',
        icon: 'warning',
        title: message,
        showConfirmButton: false,
        timer: 2000
    });
};

window.confirmDeleteDialog = () => {
    return Swal.fire({
        title: "Delete?",
        text: "Are you sure you want to delete this department?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Yes, delete"
    }).then(result => result.isConfirmed);
}
