import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';

interface User {
  // Define User properties
}

interface Template {
  // Define Template properties
}

interface Application {
  id: string;
  name: string;
  userId: string;
  rawText: string;
  createdAt: string;
  updatedAt: string;
  templateId: string;
  formResponses: string;
  user: User;
  template: Template;
}

@Component({
  selector: 'app-applications',
  templateUrl: './applications.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class ApplicationsComponent implements OnInit {
  applications: Application[] = [];
  newApplication: Application | undefined;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  ngOnInit() {
    this.http.get<Application[]>(this.baseUrl + 'api/applications').subscribe(result => {
      this.applications = result;
    }, error => console.error(error));
  }

  addNewApplication() {
    console.log('adding new application')
    this.newApplication = {
      id: crypto.randomUUID(), // Replace with actual logic to generate a unique ID
      name: 'Personal Loan',
      userId: crypto.randomUUID(), // Replace with actual logic to generate a unique User ID
      rawText: 'This is a personal loan application.',
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString(),
      templateId: 'new-template-id', // Replace with actual logic to generate a unique Template ID
      formResponses: '{"question1":"answer1","question2":"answer2"}',
      user: { /* Initialize User properties */ },
      template: { /* Initialize Template properties */ }
    };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http.post<Application>(this.baseUrl + 'api/applications', JSON.stringify('meow'), { headers }).subscribe(result => {
      this.applications.push(result);
    }, error => console.error(error));
  }
}

