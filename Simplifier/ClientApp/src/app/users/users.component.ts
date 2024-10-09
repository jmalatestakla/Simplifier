import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

interface User {
  uuid: string;
  email: string;
  isEditing?: boolean;
}

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
})
export class UsersComponent implements OnInit {
  userForm = new FormGroup({
    email: new FormControl(''),
  });

  users: User[] = [];
  newUser: User | undefined;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  async ngOnInit() {
    await this.getUsers();
  }

  addNewUser() {
    console.log('adding new user');
    this.newUser = {
      uuid: crypto.randomUUID(), 
      email: this.userForm.value.email || "undefined@gmail.com"
    };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http.post<User>(this.baseUrl + 'api/users', JSON.stringify(this.newUser), { headers }).subscribe(result => {
      this.users.push(result);
      this.getUsers();
      this.userForm.reset();
    }, error => console.error(error));
  }

  getUsers() {
    this.http.get<User[]>(this.baseUrl + 'api/users').subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }

  deleteUser(uuid: string) {
    console.log('deleting user with uuid: ' + uuid);
    this.http.delete<User>(this.baseUrl + `api/users/${uuid}`).subscribe(result => {
      this.getUsers();
    }, error => console.error(error));

  }

  async updateUser(user: User) {
    console.log('updating user with uuid: ' + user.uuid);
    this.http.put<User>(this.baseUrl + `api/users`, user, { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) }).subscribe(result => {
      this.getUsers();
    }, error => console.error(error));
    user.isEditing = false;
  }
}

