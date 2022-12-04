import React, { useState } from 'react';
import axios from 'axios';
import { setUserSession, useFormInput } from './Utils/Common';

function Login(props) {
  const [loading, setLoading] = useState(false);
  const iin = useFormInput('');
  const password = useFormInput('');
  const [error, setError] = useState(null);

  // handle button click of login form
  const handleLogin = () => {
    setError(null);
    setLoading(true);
    axios.post('http://localhost:5001/api/employees/login',
        {
          iin: iin.value,
          password: password.value
        }).then(response => {
      setLoading(false);
      setUserSession(response.data.accessToken, response.data.user);
      console.log(response.data.accessToken)
      props.history.push('/dashboard');
    }).catch(error => {
      setLoading(false);
      if (error.response.status === 401) setError(error.response.data.message);
      else setError("Something went wrong. Please try again later.");
    });
  }

  return (
    <div className ="log">
      Login<br /><br />
      <div>
        Username<br />
        <input type="text" {...iin} autoComplete="new-password" />
      </div>
      <div style={{ marginTop: 10 }}>
        Password<br />
        <input type="password" {...password} autoComplete="new-password" />
      </div>
      {error && <><small style={{ color: 'red' }}>{error}</small><br /></>}<br />
      <input type="button" value={loading ? 'Loading...' : 'Login'} onClick={handleLogin} disabled={loading} /><br />
    </div>
  );
}

export default Login;