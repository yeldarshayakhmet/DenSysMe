import React, { useState, useEffect } from 'react';
import { BrowserRouter, Switch, Route, NavLink } from 'react-router-dom';
import axios from 'axios';

import Login from './Login';
import Dashboard from './Dashboard';
import Home from './Home';
import Patient from './components/Patient/Patient';

import AddPatient from './components/Patient/AddPatient';
import EditPatient from './components/Patient/EditPatient';
import DoctorsList from "./components/Doctor/DoctorsList";
import AddDoctor from './components/Doctor/AddDoctor';
import EditDoctor from './components/Doctor/EditDoctor';
import GetDoctor from './components/Doctor/getDoctor';
import BookAppointment from './components/Patient/bookAppointment';



import Patientlist from './components/Patient/Patientlist';
import PrivateRoute from './Utils/PrivateRoute';
import PublicRoute from './Utils/PublicRoute';
import { getToken, removeUserSession, setUserSession } from './Utils/Common';

function App() {
/*  const [authLoading, setAuthLoading] = useState(true);

  useEffect(() => {
    const token = getToken();
    if (!token) {
      return;
    }

    axios.get(`http://localhost:4000/verifyToken?token=${token}`).then(response => {
      setUserSession(response.data.token, response.data.user);
      setAuthLoading(false);
    }).catch(error => {
      removeUserSession();
      setAuthLoading(false);
    });
  }, []);

  if (authLoading && getToken()) {
    return <div className="content">Checking Authentication...</div>
  }
*/
  return (
    <div className="App">
      <BrowserRouter>
        <div>
          <div className="header">
            <NavLink exact activeClassName="active" to="/">Home</NavLink>
            <NavLink activeClassName="active" to="/login">Login</NavLink>
            <NavLink activeClassName="active" to="/dashboard">Dashboard</NavLink>
            <NavLink exact activeClassName="active" to="/doctors">Doctor</NavLink>
            <NavLink exact activeClassName="active" to="/patientlist">Patient</NavLink>
            <NavLink exact activeClassName="active" to="/getdoctor">Request</NavLink>
            <NavLink exact activeClassName="active" to="/patient"></NavLink>
          </div>
          <div className="content">
            <Switch>
              <Route exact path="/" component={Home} />
              <PublicRoute path="/login" component={Login} />
              <PrivateRoute path="/dashboard" component={Dashboard} />
              <Route exact path="/doctors" component={DoctorsList} />
              <Route exact path="/patient" component={Patient} />
              <Route exact path="/request" component={Request} />
              <Route exact path="/addpatient" component={AddPatient} />
              <Route exact path="/editpatient/:iin" component={EditPatient} />
              <Route exact path="/adddoctor" component={AddDoctor} />
              <Route exact path="/editdoctor/:iin" component={EditDoctor} />
              <Route exact path ="/bookAppointment/:doctorId" component={BookAppointment} />
              <Route exact path ="/getdoctor" component={GetDoctor} />
              <Route exact path='/patientlist' component={Patientlist}/>
            </Switch>
          </div>
        </div>
      </BrowserRouter>
    </div>
  );
}

export default App;
