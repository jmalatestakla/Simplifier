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
  fieldName: string;
  fieldType: string;
  expectedValue: string;
  order: number;
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
  selector: 'app-templates-component',
  templateUrl: './templates.component.html',
})
export class TemplatesComponent {
  templates: Template[] = [];
  selectedTemplate: Template | undefined;
  selectedTemplateItems: formField[] = [];
  templateName: string = '';
  templateItems: TemplateItem[] = [];
  formInvalid: boolean = false;
  newTemplateName: string = '';
  formFields: formField[] = [];
  templateForm = this.fb.group({
    name: ['', Validators.required],
    fieldName: ['', Validators.required],
    fieldType: ['', Validators.required],
    expectedValue: ['', Validators.required],
  });
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
    const order = this.templateItems.length + 1;
    this.templateItems.push({...this.templateForm.value, order} as TemplateItem);
  }

  selectTemplate(template: Template) {
    this.selectedTemplate = template;
    console.log(this.selectedTemplate.formFields);
  }

  async ngOnInit() {
    await this.getTemplates(); // wait for getTemplates to finish
    if (this.templates.length > 0) {
      this.templates.sort(
        (a, b) =>
          new Date(b.updatedAt).getTime() - new Date(a.updatedAt).getTime()
      );
      this.selectedTemplate = this.templates[0];
    } 
  }

  async getTemplates() {
    this.templates =
      (await this.http
        .get<Template[]>(this.baseUrl + 'api/templates')
        .toPromise()) || [];
        console.log(this.templates);
  }

  // Save entire form on backend.
  saveTemplate() {
    console.log('saving template');
    console.log(this.templateItems);
    let templateId: string = crypto.randomUUID()
    let template = {
      Uuid: templateId, // Uuid with uppercase
      UserId: 'b1cd0b7c-210a-4170-85f2-009c11fe3baa',
      Name: this.templateForm.value.name || 'undefined',
      FormFields: this.templateItems.map(item => ({
        Uuid: crypto.randomUUID(), // Uuid with uppercase
        FormField: item.fieldName,
        FormType: item.fieldType,
        ExpectedResponse: item.expectedValue,
        Order: item.order,
        TemplateId: templateId // TemplateId with uppercase
      })),
      CreatedAt: new Date(),
      UpdatedAt: new Date(),
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

  // updateTemplate() {
  //   console.log('saving template');
  //   let template: Template = {
  //     uuid: crypto.randomUUID(),
  //     userId: 'b1cd0b7c-210a-4170-85f2-009c11fe3baa',
  //     name: 'test',
  //     formFields: JSON.stringify(this.templateItems),
  //     createdAt: new Date(),
  //     updatedAt: new Date(),
  //   };
  //   const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  //   this.http
  //     .put<Template>(
  //       this.baseUrl + 'api/templates',
  //       JSON.stringify(template),
  //       { headers }
  //     )
  //     .subscribe(
  //       (result) => {
  //         this.templates.push(result);
  //         this.getTemplates();
  //         this.templateForm.reset();
  //       },
  //       (error) => console.error(error)
  //     );
  // }
}
