$(document).ready(function(){
    const _login = document.getElementById('btn-login')
    const _signUp = document.getElementById('btn-signup')
    const _conditions = document.getElementById('btn-conditions')

    const _name = document.getElementById('name')
    const _username = document.getElementById('username')
    const _gender = document.getElementById('gender')
    const _birthday = document.getElementById('birthday')
    const _phone = document.getElementById('phone')
    const _country = document.getElementById('country')
    const _email = document.getElementById('email')
    const _password = document.getElementById('password')

    const _labelUsername = document.getElementById('label-username')
    const _labelEmail = document.getElementById('label-email')

    const _divUsername = document.getElementById('div-username')
    const _divEmail = document.getElementById('div-email')

    _login.addEventListener('click', function(){
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

    var timerUser
    _username.addEventListener('keyup', function(){
        clearTimeout(timerUser)

        timerUser = setTimeout(function(){
            var username = _username.value
            if(username.length > 5){
                $.ajax({
                    url: "/Home/CheckUsername",
                    data: {username: username},
                    method: "POST",
                    success: function(result){
                        console.log(result)
                        if(!result){
                            _labelUsername.innerHTML = "Username already in use"
                            _divUsername.style.backgroundColor = "#e92828"
                        }
                        else{
                            _labelUsername.innerHTML = "Username"
                            _divUsername.style.backgroundColor = "var(--blue-charcoal)"
                        }
                    },
                    error: function(e){
                        console.log(e)
                    }
                })
            }
            else{
                _labelUsername.innerHTML = "Username must be at least 6 characters long"
                _divUsername.style.backgroundColor = "#e92828"
            }
        }, 2000)
    })

    var timerEmail
    _email.addEventListener('keyup', function(){
        clearTimeout(timerEmail)

        timerEmail = setTimeout(function(){
            var email = _email.value
            if(email.includes('@')){
                $.ajax({
                    url: "/Home/CheckEmail",
                    data: {email: email},
                    method: "POST",
                    success: function(result){
                        console.log(result)
                        if(!result){
                            _labelEmail.innerHTML = "Email already in use"
                            _divEmail.style.backgroundColor = "#e92828"
                        }
                        else{
                            _labelEmail.innerHTML = "Email"
                            _divEmail.style.backgroundColor = "var(--blue-charcoal)"
                        }
                    },
                    error: function(e){
                        console.log(e)
                    }
                })
            }
            else{
                _labelEmail.innerHTML = "Writing a correct email"
                _divEmail.style.backgroundColor = "#e92828"
            }
        }, 2000)
    })

    _signUp.addEventListener('click', function(){
        var name = _name.value
        var username = _username.value
        var gender = _gender.value
        var birthday = _birthday.value
        var phone = _phone.value
        var country = _country.value
        var email = _email.value
        var password = _password.value

        if(name != '' && username != '' && gender != '' && birthday != '' && phone != '' && country != '' && email != '' && password != ''){
            var data = {
                "CompleteName": name,
                "Username": username,
                "Gender": gender,
                "Birthday": birthday,
                "Phone": phone,
                "Country": country,
                "Email": email,
                "Password": password
            }
            
            const json = JSON.stringify(data)

            $.ajax({
                url: "/Home/Register",
                data: {json: json},
                method: "POST",
                success: function(){
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
        else{
            alert('Fill in all fields.')
        }
    })   
    
    getGenders()
    function getGenders(){
        $.ajax({
            url: "/Home/GetGenders",
            method: "POST",
            success: function(result){
                var options = ""
                result.forEach(function(key){
                    options += '<option value = "' + key + '">' + key.charAt(0).toUpperCase() + key.slice(1); + '</option>'
                })

                $("#gender").append(options)
            },
            error: function(e){
                console.log(e)
            }
        })
    }
})