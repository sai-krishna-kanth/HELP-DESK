import React, { Component } from 'react';

import { setAccessToken, setUser, isLoggedIn } from './helpers';




export class LoginView extends Component {
    static displayName = LoginView.name;

    constructor(props) {
        super(props);
        this.state = { deskLogin: '', deskPassword: '', loggedIn: false };
        //this.handleSubmit = this.handleSubmit(this);
        this.handleOnChange = this.handleOnChange.bind(this);
        this.prepareFormData = this.prepareFormData.bind(this);
        this.loginUser = this.loginUser.bind(this);
        this.checkStatus = this.checkStatus.bind(this);
    }

    /*
    handleOnChange(event) {
        this.setState({ [event.target.id]: event.target.value, errors: [] });
    }
    */

    handleOnChange(event) {
        const target = event.target;
        const name = target.name;
        //const userPassword = target.userPassword;
    }
    /*
    handleSubmit(event) {
        alert('HelpDesk User: ' + this.state.newRegister + ' has signed in!  yay?');
    }
    */
    prepareFormData(data = this.state) {
        return {
            UserName: data.userName.trim(),
            Password: data.password.trim()
        };
    }

    loginUser(event) {

        var data = JSON.stringify(this.prepareFormData());

        // Send POST request with data submited from form
        fetch('api/users/login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json, application/xml, text/plain, text/html, *.*',
                'Content-Type': 'application/json; charset=UTF-8'
            },
            body: data
        })
            .then(this.checkStatus);
    }

    checkStatus(res) {
        if (res.status >= 200 && res.status < 300) {
            setAccessToken(res.access_token);
            setUser(this.state.userName);
            this.setState({ loggedIn: true });
            this.props.history.push('/User1');
        } else {
            let error = new Error(res.statusTest);
            console.log(error);
            this.props.history.push('/Counter');
        }
    }




    render() {
        if (this.state.loggedIn) {
            window.location.replace("/");
            return true;
        }
        const isLoggedIn = this.state.loggedIn;
        //let contents = this.state.loading ? <p><em>Loading Users....</em></p> : LoginView.renderUsers(this.state.login);
        //let { logginIn } = this.props;
        //let { username, password, submitted } = this.state;

        return (

 
            <div class="">
                <h1 id="tabelLabel" >HelpDesk Login</h1>
                <h2>Email and password please.....</h2>
                <p>-------------------------------</p>
                <p>Seeded data.  Use test users below</p>
                <p>test1@test.com    password1</p>
                <p>test2@test.com    password2</p>
                <p>admin@helpdeskteammember.com    password3</p>

                <p>-------------------------------</p>
                <select>
                    <option>Help Desk USER.</option>
                    <option>Help Desk ADMIN only.</option>
                    <option>The Matrix.</option>
                </select>
                < br />
                < br />

                <form onSubmit={this.loginUser} action='tickets-view'>
                    <div className={'form-group mx-sm-3 mb-2'}>
                        <label asp-for="userName" htmlFor="userName" class="form-group" >Login email: </label>
                        <input name="deskLogin" class="form-group" asp-for="userName" onChange={this.handleOnChange} placeholder="  user@helpdesk.com" />            
                            <div className="help-block">UserName is required</div>
                    </div>
                    <div className={'form-group mx-sm-3 mb-2'}>
                        <label asp-for="password" htmlFor="password" class="form-group" >Password: </label>
                        <input name="deskPassword" class="form-group" asp-for="password" type="password" onChange={this.handleOnChange} placeholder="  123PassWord" />
                            <p className="help-block">Password is required</p>
                    </div>
                    < br />
                    <div>
                        The user is <b>{isLoggedIn ? 'currently' : 'not'}</b> logged in.
                    </div>
                    <button type="submit" className="btn btn-primary"> Login </button>



                </form>
                <h6>Welcome: {this.UserData}</h6>
      
            </div>

        );
    }
}



