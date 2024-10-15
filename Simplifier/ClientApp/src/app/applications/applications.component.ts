import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

interface Application {
  uuid: string; // PK
  userId: string; // FK
  name: string;
  rawText: string;
  createdAt: string;
  updatedAt: string;
  templateId: string; // FK
  formResponses: string;
}

@Component({
  selector: 'app-applications',
  templateUrl: './applications.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
})
export class ApplicationsComponent implements OnInit {
  applications: Application[] = [];
  newApplication: Application | undefined;
  templates: any[] = [];
  selectedApplication: Application | undefined;


  applicationForm = this.fb.group({
    name: ['', Validators.required],
    rawText: ['', Validators.required],
    templateId: ['', Validators.required],
  });
  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}


  ngOnInit() {
    this.http.get<Application[]>(this.baseUrl + 'api/applications').subscribe(result => {
      this.applications = result;
    }, error => console.error(error));
  this.http.get<any[]>(this.baseUrl + 'api/templates').subscribe(result => {
    this.templates = result;
  }, error => console.error(error));
  }

  addNewApplication() {
    console.log('adding new application')
    if (this.applicationForm.invalid) {
      console.log('form invalid');
      return;
    }

    this.newApplication = {
      uuid: crypto.randomUUID(), // Replace with actual logic to generate a unique ID
      name: this.applicationForm.value.name!,
      userId: crypto.randomUUID(), // Replace with actual logic to generate a unique User ID
      rawText: 'This is a personal loan application.',
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString(),
      templateId: 'new-template-id', // Replace with actual logic to generate a unique Template ID
      formResponses: '{"question1":"answer1","question2":"answer2"}',
    };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http.post<Application>(this.baseUrl + 'api/applications', JSON.stringify('meow'), { headers }).subscribe(result => {
      this.applications.push(result);
    }, error => console.error(error));
  }
}

