$(document).ready(function () {
    const _username = document.getElementById('username')
    const _password = document.getElementById('password')
    
    const _login = document.getElementById('login')
    const _register = document.getElementById('register')
    
    _login.addEventListener("click", function() {
        var username = _username.value
        var password = _password.value

        if(username != "" && password != ""){
            $.ajax({
                url: "/Home/Logon",
                data: {username: username, password: password},
                method: "POST",
                success: function(result){
                    $.ajax({
                        url: "/Home/GoToHomePage",
                        method: "POST",
                        success: function(result){
                            window.location.href = result.redirectToUrl;
                        },
                        error: function(e){
                            console.log(e)
                        }
                    })                     
                },
                error: function(e){
                    console.log(e)
                }
            })
        }
    })

    _register.addEventListener('click', function(){
        $.ajax({
            url: "/Home/GoToSignUp",
            method: "POST",
            success: function(result){
                window.location.href = result.redirectToUrl;
            },
            error: function(e){
                console.log(e)
            }
        })
    })

})