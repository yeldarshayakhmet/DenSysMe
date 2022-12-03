
import React, { useState, useEffect} from 'react';
import "bootstrap/dist/css/bootstrap.min.css";
import {Link} from 'react-router-dom';

import {Button} from 'reactstrap'

const Patient = () => {
  const [docdata, docdatachange] = useState(null);

  useEffect(() => {
      fetch("http://localhost:3000/patients").then((res) => {
          return res.json();
      }).then((resp) => {
          docdatachange(resp);
      }).catch((err) => {
          console.log(err.message);
      })
  }, [])
  return (
      <div>
      <div className="container">
          <div className="card">
              <div className="card-title">
                  <h2>Patient Listing</h2>
              </div>
              <div className="card-body">
                  <div className="divbtn">
                  <Link to = "/adddoctor" className="btn btn-primary"> Add new Patient (+)</Link>
                  </div>
                  <table className="table table-bordered">
                      <thead className="bg-dark text-white">
                          <tr>
                              <td>INN</td>
                              <td>Name</td>
                              <td>Surname</td>
                              <td>Email</td>
                              <td>Phone</td>
                              <td>Action</td>
                          </tr>
                      </thead>
                      <tbody>

                          {docdata &&
                              docdata.map(item => (
                                  <tr key={item.inn}>
                                      <td>{item.inn}</td>
                                      <td>{item.name}</td>
                                      <td>{item.surname}</td>
                                      <td>{item.email}</td>
                                      <td>{item.phone}</td>
                                      <td>
                               </td>
                                      
                                  </tr>
                              ))
                          }
                      </tbody>

                  </table>
              </div>
          </div>
      </div>
      <Link to = "/editpatient/1" className="btn btn-warning"> Edit</Link>
      <Button color='danger'>Delete</Button>
    
  </div>
  
);
}

export default Patient;

