import React, { Component } from 'react';
import axios from 'axios';
import { setAccessToken, setUser, isLoggedIn } from './helpers';





export class RegistrationView extends Component {
    static displayName = RegistrationView.name;

    constructor(props) {
        super(props);
        this.state = { newRegister: [], newPassword: '', newConfirmPassword: '', loggedIn: false };
        this.handleOnChange = this.handleOnChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this); //added oct 14
        this.prepareFormData = this.prepareFormData.bind(this);
        this.registerUser = this.registerUser.bind(this);
        this.checkStatus = this.checkStatus.bind(this);
    }

    /*
    handleOnChange(event) {
        this.setState({ [event.target.id]: event.target.value, errors: [] });
    }
    */

    handleOnChange=(event)=>{
        this.setState({
            [event.target.name]: event.target.value
        })
        //const target = event.target;
        //const name = target.name;
    }

    handleSubmit(event) {
        event.preventDefault();
        const { newPassword, newConfirmPassword } = this.state;
        if (newPassword !== newConfirmPassword) {
            alert("Passwords do NOT match");
        }
        else {
            alert('You have registered:  ' + this.state.newRegister);
        }
    }

    prepareFormData(data = this.state) {
        return {
            UserName: data.userName.trim(),
            Password: data.password.trim(),
            ConfirmPassword: data.confirmpassword.trim()
        };
    }

    registerUser(event) {

        var data = JSON.stringify(this.prepareFormData());

        // Send POST request with data submited from form
        fetch('Register', {
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

    componentDidMount() {
        axios.get('https://jsonplaceholder.typicode.com/users')
            .then(res => {
                const newRegister = res.data;
                this.setState({ newRegister });
            })
    }


    render() {
        if (this.state.loggedIn) {
            window.location.replace("/");
            return true;
        }
        //let contents = this.state.loading ? <p><em>Loading Users....</em></p> : LoginView.renderUsers(this.state.login);
        //let { logginIn } = this.props;
        //let { username, password, submitted } = this.state;

        return (

 
            <div class="">
                <h1 id="tabelLabel" >HelpDesk REGISTRATION</h1>
                <h2>Email, password, and password confirmation please.....</h2>
                <p>-------------------------------</p>
                <p>Seeded data.  Use test users below</p>
                <p>test1@test.com    password1</p>
                <p>test2@test.com    password2</p>
                <p>admin@helpdeskteammember.com    password3</p>
                <ul>
                    {this.state.newRegister.map(registrants => <li>{registrants.name}</li>)}
                </ul>

                <p>-------------------------------</p>


                <form onSubmit={this.registerUser} action='tickets-view'>
                    <div className={'form-group mx-sm-3 mb-2'}>
                        <label htmlFor="userName" class="form-group">Login email: </label>
                        <input name="newRegister" type="text" onChange={this.handleOnChange} class="form-group" placeholder=" user email" />
                        <h5 asp-validation-for="Email"></h5>
                        {
                            <div className="help-block">UserName is required</div>
                        }
                    </div>
                    <div className={'form-group mx-sm-3 mb-2'}>
                        <label htmlFor="password" class="form-group">Password: </label>
                        <input name="newPassword" class="form-group" type="password" onChange={this.handleOnChange} placeholder="  Password" />
                        {
                            <div className="help-block">Password is required</div>
                        }
                    </div>
                    <div className={'form-group mx-sm-3 mb-2'}>
                        <label htmlFor="confirmpassword" class="form-group">Confirm Password: </label>
                        <input name="newConfirmPassword" class="form-group" type="password" onChange={this.handleOnChange} placeholder="  Password Confirm" />
                        {
                            <div className="help-block">Confirmation Password is required</div>
                        }
                    </div>
                    <button type="submit" className="btn btn-primary" > Resigster </button>


                </form>
      
            </div>

        );
    }
}



