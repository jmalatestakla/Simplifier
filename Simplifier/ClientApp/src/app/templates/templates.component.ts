import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {
  Component,
  Inject,
  Input,
  SimpleChange,
  SimpleChanges,
} from '@angular/core';
import { Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';

interface TemplateItem {
  fieldName: string | null;
  fieldType: string | null;
  expectedValue: string | null;
}

interface Template {
  uuid: string;
  userId: string;
  name: string;
  formFields: string;
  createdAt: Date;
  updatedAt: Date;
}

@Component({
  selector: 'app-templates-component',
  templateUrl: './templates.component.html',
  imports: [ReactiveFormsModule, CommonModule],
  standalone: true,
})
export class TemplatesComponent {
  templates: Template[] = [];
  selectedTemplate: Template | undefined;
  selectedTemplateItems: TemplateItem[] = [];
  templateName: string = '';
  templateItems: TemplateItem[] = [];
  formInvalid: boolean = false;
  newTemplateName: string = '';
  templateForm = this.fb.group({
    name: ['', Validators.required],
    fieldName: ['', Validators.required],
    fieldType: ['', Validators.required],
    expectedValue: ['', Validators.required],
  });
JSON: any;
  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  // Form in the template creator. Adds a new field to the list.
  addNewItem(): void {
    // Validation
    if (this.templateForm.invalid) {
      console.log('form invalid');
      this.formInvalid = true;
      return;
    }
    // Add new form field on frontend
    console.log('submitted form');
    this.templateItems.length === 0
    this.templateItems.push(this.templateForm.value as TemplateItem);
    console.log(this.templateItems.length);
  }

  selectTemplate(template: Template) {
    this.selectedTemplate = template;
    console.log(template.formFields);
    this.selectedTemplateItems = JSON.parse(template.formFields) as TemplateItem[];
  }

  async ngOnInit() {
    await this.getTemplates(); // wait for getTemplates to finish
    if (this.templates.length > 0) {
      this.templates.sort(
        (a, b) =>
          new Date(b.updatedAt).getTime() - new Date(a.updatedAt).getTime()
      );
      this.selectedTemplate = this.templates[0];
      this.selectedTemplateItems = JSON.parse(this.templates[0].formFields) as TemplateItem[];
    }
  }

  async getTemplates() {
    this.templates =
      (await this.http
        .get<Template[]>(this.baseUrl + 'api/templates')
        .toPromise()) || [];
  }

  // Save entire form on backend.
  saveTemplate() {
    console.log('saving template');
    console.log(this.newTemplateName);
    let template: Template = {
      uuid: crypto.randomUUID(),
      userId: 'b1cd0b7c-210a-4170-85f2-009c11fe3baa',
      name: this.templateForm.value.name || 'undefined',
      formFields: JSON.stringify(this.templateItems),
      createdAt: new Date(),
      updatedAt: new Date(),
    };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http
      .post<Template>(
        this.baseUrl + 'api/templates',
        JSON.stringify(template),
        { headers }
      )
      .subscribe(
        (result) => {
          this.templates.push(result);
          this.getTemplates();
          this.templateForm.reset();
        },
        (error) => console.error(error)
      );

      // reset things
      this.templateItems = [];
      this.templateForm.reset();
      this.templateName = '';
  }

  updateTemplate() {
    console.log('saving template');
    let template: Template = {
      uuid: crypto.randomUUID(),
      userId: 'b1cd0b7c-210a-4170-85f2-009c11fe3baa',
      name: 'test',
      formFields: JSON.stringify(this.templateItems),
      createdAt: new Date(),
      updatedAt: new Date(),
    };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http
      .put<Template>(
        this.baseUrl + 'api/templates',
        JSON.stringify(template),
        { headers }
      )
      .subscribe(
        (result) => {
          this.templates.push(result);
          this.getTemplates();
          this.templateForm.reset();
        },
        (error) => console.error(error)
      );
  }
}
