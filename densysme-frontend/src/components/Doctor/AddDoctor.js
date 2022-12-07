import React from 'react';
import {api, getToken} from "../../Utils/Common";
import {Form, Input, Label} from "reactstrap";

class AddDoctor extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            iin: '',
            firstName: '',
            middleName: '',
            lastName: '',
            dateOfBirth: '',
            phone: '',
            email: '',
            yearsOfExperience: 0,
            appointmentPrice: 0.0,
            address: '',
            category: 1,
            degree: 1,
            specializationId: 0,
            specializations: []
        };
        this.createDoctor = this.createDoctor.bind(this);
        api.get("specializations", {
            headers: {
                'Authorization': 'Bearer ' + getToken()
            }
        }).then(res => {
            console.log(res.data);
            this.setState({ specializations: res.data, specializationId: res.data[0].id });
        });
    }
    
    createDoctor(event) {
        event.preventDefault();
        let form = new FormData();
        form.append('request', JSON.stringify(this.state));
        form.append('photo', document.getElementById("photo").files[0])
        api.post('employees/doctors', form, {
            headers: {
                'Authorization': 'Bearer ' + getToken(),
                'Content-Type': 'multipart/form-data'
            }
        }).then(res => console.log(res)).catch(err => console.log(err));
    }

    /*function AddDoctor() {
    const dateOfBirth = useFormInput('');
    const iin = useFormInput('');
    const firstName = useFormInput('');
    const lastName = useFormInput('');
    const phone = useFormInput('');
    const yearsOfExperience = useFormInput('');
    const appointmentPrice = useFormInput('');
    const address = useFormInput('');
    
    const handleCreateDoctor = () => {
    axios.post("http://localhost:5001/api/employees/doctors", {
      request: {
        dateOfBirth: dateOfBirth.value,
        iin: iin.value,
        firstName: firstName.value,
        lastName: lastName.value,
        phone: phone.value,
        yearsOfExperience: yearsOfExperience.value,
        appointmentPrice: appointmentPrice.value,
        address: address.value
      }
    }, {
      headers: {
        'Authorization': 'Bearer ' + getToken(),
        'Content-Type': 'multipart/form-data'
      }
    })
    }*/
    render() {
        return (
            <Form onSubmit={this.createDoctor}>
                <div>
                    <Label>IIN</Label>
                    <Input
                        type="text"
                        value={this.state.iin}
                        onChange={event => {
                            this.setState({iin: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <Label>FirstName</Label>
                    <Input
                        type="text"
                        value={this.state.firstName}
                        onChange={event => {
                            this.setState({firstName: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <Label>LastName</Label>
                    <Input
                        type="text"
                        value={this.state.lastName}
                        onChange={event => {
                            this.setState({lastName: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <Label>MiddleName</Label>
                    <Input
                        type="text"
                        value={this.state.middleName}
                        onChange={event => {
                            this.setState({middleName: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <Label>Date Of Birth</Label>
                    <Input
                        type="date"
                        value={this.state.dateOfBirth}
                        onChange={event => {
                            this.setState({dateOfBirth: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <Label>Phone</Label>
                    <Input
                        type="text"
                        value={this.state.phone}
                        onChange={event => {
                            this.setState({phone: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <Label>Email</Label>
                    <Input
                        type="text"
                        value={this.state.email}
                        onChange={event => {
                            this.setState({email: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <Label>Years Of Experience</Label>
                    <Input
                        type="number"
                        value={this.state.yearsOfExperience}
                        onChange={event => {
                            this.setState({yearsOfExperience: parseInt(event.target.value)})
                        }}
                    />
                </div>
                <div>
                    <Label>Appointment Price</Label>
                    <Input
                        type="number"
                        value={this.state.appointmentPrice}
                        onChange={event => {
                            this.setState({appointmentPrice: parseFloat(event.target.value)})
                        }}
                    />
                </div>
                <div>
                    <Label>Address</Label>
                    <Input
                        type="text"
                        value={this.state.address}
                        onChange={event => {
                            this.setState({address: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <Label> Degree</Label>
                    <select value={this.state.degree} onChange={event => { this.setState({degree: parseInt(event.target.value)}) }}>
                        <option value = {1}>MD</option>
                        <option value = {2}>PhD</option>
                        <option value = {3}>Master</option>
                        <option value = {4}>Bachelor</option>
                    </select>
                </div>
                <div>
                    <Label> Category</Label>
                    <select value={this.state.category} onChange={event => { this.setState({category: parseInt(event.target.value)}) }}>
                        <option value = {1}>First</option>
                        <option value = {2}>Second</option>
                        <option value = {3}>Highest</option>
                        <option value = {4}>None</option>
                    </select>
                </div>
                <div>
                    <Label> Specialization</Label>
                    <select value = {this.state.specializationId} onChange={event => { this.setState({ specializationId: event.target.value }) }}> {
                            this.state.specializations.map(spec => <option key={spec.id} value = {spec.id}>{spec.name}</option>)
                        }
                    </select>
                </div>
                <div>
                    <Label>Photo</Label>
                    <Input
                        type="file"
                        id="photo"
                        accept="image/jpeg"
                    />
                </div>
                <button type="submit">Create</button>
            </Form>
        );
    }
}

export default AddDoctor;