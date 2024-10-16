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
  formResponses: FormResponse[];
}

interface FormResponse {
  uuid: string; // PK
  applicationId: string; // FK
  formField: string;
  response: string;
}

interface Template {
  uuid: string;
  userId: string;
  name: string;
  createdAt: Date;
  updatedAt: Date;
  formFields: formField[];
}
interface formField {
  uuid: string;
  templateId: string;
  formField: string;
  formType: string;
  expectedResponse: string;
  order: number;
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
  selectedApplicationTemplateName: string = '';
  responses: FormResponse[] = [];
  toggleRawText: boolean = false;

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

  async getData() {
    this.applications =
    (await this.http
      .get<Application[]>(this.baseUrl + 'api/applications')
      .toPromise()) || [];
    this.templates =
    (await this.http
      .get<Template[]>(this.baseUrl + 'api/templates')
      .toPromise()) || [];
  }

  async ngOnInit() {
    await this.getData(); // wait for getTemplates to finish
    if (this.templates.length > 0) {
      this.templates.sort(
        (a, b) =>
          new Date(b.updatedAt).getTime() - new Date(a.updatedAt).getTime()
      );
    } 
  }

  selectApplication(application: Application | undefined) {
    this.selectedApplication = application;
    console.log(this.templates);
    this.selectedApplicationTemplateName = this.templates.find(template => template.uuid === application?.templateId)?.name || '';
    this.toggleRawText = false;
  }

  addNewApplication() {
    if (this.applicationForm.invalid) {
      console.log('form invalid');
      return;
    }
    this.responses = [
      {
        uuid: crypto.randomUUID(),
        applicationId: crypto.randomUUID(), // Replace with actual logic to generate a unique Application ID
        formField: 'field1',
        response: 'response1',
      },
      {
        uuid: crypto.randomUUID(),
        applicationId: crypto.randomUUID(), // Replace with actual logic to generate a unique Application ID
        formField: 'field2',
        response: 'response2',
      },
    ];

    this.newApplication = {
      uuid: crypto.randomUUID(), // Replace with actual logic to generate a unique ID
      name: this.applicationForm.value.name!,
      userId: 'b1cd0b7c-210a-4170-85f2-009c11fe3baa', // Replace with actual logic to generate a unique User ID
      rawText: this.applicationForm.value.rawText!,
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString(),
      templateId: this.applicationForm.value.templateId!, // Replace with actual logic to generate a unique Template ID
      formResponses: this.responses,
    };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http
      .post<Application>(
        this.baseUrl + 'api/applications',
        JSON.stringify(this.newApplication),
        { headers }
      )
      .subscribe(
        (result) => {
        },
        (error) => console.error(error)
      );
    this.applications.push(this.newApplication);
    this.applicationForm.reset();
  }


  deleteApplication() {
    if (!this.selectedApplication) {
      console.log('No application selected');
      return;
    }

    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http
      .delete(this.baseUrl + `api/applications/${this.selectedApplication.uuid}`, { headers })
      .subscribe(
        () => {
          this.applications = this.applications.filter(app => app.uuid !== this.selectedApplication!.uuid);
          this.selectedApplication = undefined;
          console.log('Application deleted successfully');
        },
        (error) => console.error(error)
      );
  }
}
