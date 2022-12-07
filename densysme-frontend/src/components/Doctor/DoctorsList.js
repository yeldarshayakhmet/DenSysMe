import React from "react";
import axios from "axios";
import {
	Table,
	Nav,
	NavItem,
	NavLink,
	Button,
	Row,
	Col,
	Input,
} from "reactstrap";
import { api, getToken } from '../../Utils/Common'
import { Link } from "react-router-dom";
class DoctorsList extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			doctors: [],
			searchTerm: "",
		};
		this.handleDelete = this.handleDelete.bind(this);
		this.getDoctors = this.getDoctors.bind(this);
	}
	
	async componentDidMount() {
		await this.getDoctors();
	}
	
	async getDoctors() {
		const headers = {
			Authorization: 'Bearer ' + getToken(),
		};
		await api.get("employees/doctors?search=" + this.state.searchTerm, { headers: headers })
			.then((res) => {
				console.log(res);
				this.setState({ doctors: res.data });
			});
	}
	
	handleDelete(id) {
		console.log(id);

		axios
			.post("http://localhost:12347/deleteDoctor", {
				Id: id,
			})
			.then((res) => {
				alert("User deleted");
				window.location.reload(false);
			});
	}
	
	render() {
		return (
			<div>
				<Nav tabs>
					<NavItem>
						<NavLink>
							<Link to="/adddoctor">
								Add Doctor
							</Link>
						</NavLink>
					</NavItem>
					<NavItem>
						<NavLink active>
							<Link to="/doctors">
								Doctors
							</Link>
						</NavLink>
					</NavItem>
				</Nav>
				<Row>
					<Col className="mt-2 mr-5" sm="3">{" "}</Col>
					<Col className="mt-3">
						<Input
							style={{ width: "50%" }}
							placeholder="Search..."
							type="text"
							onChange={event => { this.setState({ searchTerm: event.target.value })} }
						/> <Button type="button" onClick={this.getDoctors}>Search</Button>
						<Table
							striped
							style={{
								width: "50%",
								"box-shadow": "2px 2px 4px 4px #CCCCCC",
								marginTop: "30px",
							}}
						>
							<thead>
								<tr>
									<th>Name</th>
									<th>Phone</th>
									<th>Specialization</th>
									<th>Experience</th>
									<th>Appointment Price</th>
								</tr>
							</thead>
							<tbody>
								{this.state.doctors.map(doctor =>
									<tr key={doctor.id}>
										<td>{doctor.firstName + ' ' + doctor.lastName}</td>
										<td>{doctor.phoneNumber}</td>
										<td>{doctor.specialization && doctor.specialization.name}</td>
										<td>{doctor.yearsOfExperience}</td>
										<td>{doctor.appointmentPrice}</td>
										<td>
											<Link to={{
												pathname: "/bookAppointment/:doctorId",
												doctorId: doctor.id
											}}>
												Book Appointment
											</Link>
										</td>
									</tr>
								)}
							</tbody>
						</Table>
					</Col>
				</Row>
			</div>
		);
	}
}

export default DoctorsList;
