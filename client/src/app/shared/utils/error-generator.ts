import { ApiErrorResponse } from "../models/api-response";

export abstract class ErrorGenerator {
  public static generate(message: string, parameters: string[] = []): ApiErrorResponse {
    const error: Partial<ApiErrorResponse> = {
      errorCode: message,
      parameters: parameters,
    };

    return error as ApiErrorResponse;
  }
}
