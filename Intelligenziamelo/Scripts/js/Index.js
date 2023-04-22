$(document).ready(function(){
    const _btnLogin = document.getElementById('btn-login')

    _btnLogin.addEventListener('click', function(){
        $.ajax({
            url: "/Home/GoToLogin",
            method: "POST",
            success: function(result){
                window.location.href = result.redirectToUrl
            },
            error: function(e){
                console.log(e)
            }
        })
    })
})