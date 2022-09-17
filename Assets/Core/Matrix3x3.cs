using System;
using UnityEngine;

// ------------------- copy from the library of AForge.Math
// ------------------- http://www.aforgenet.com/framework/downloads.html


/// <summary>
/// A structure representing 3x3 matrix.
/// </summary>
///
/// <remarks><para>The structure incapsulates elements of a 3x3 matrix and
/// provides some operations with it.</para></remarks>
[Serializable]
public struct Matrix3x3
{
	/// <summary>
	/// Provides an identity matrix with all diagonal elements set to 1.
	/// </summary>
	public static Matrix3x3 Identity
	{
		get
		{
			Matrix3x3 result = default(Matrix3x3);
			result.V00 = (result.V11 = (result.V22 = 1f));
			return result;
		}
	}

	/// <summary>
	/// Calculates determinant of the matrix.
	/// </summary>
	public float Determinant
	{
		get
		{
			return this.V00 * this.V11 * this.V22 + this.V01 * this.V12 * this.V20 + this.V02 * this.V10 * this.V21 - this.V00 * this.V12 * this.V21 - this.V01 * this.V10 * this.V22 - this.V02 * this.V11 * this.V20;
		}
	}

	/// <summary>
	/// Returns array representation of the matrix.
	/// </summary>
	///
	/// <returns>Returns array which contains all elements of the matrix in the row-major order.</returns>
	public float[] ToArray()
	{
		return new float[]
		{
				this.V00,
				this.V01,
				this.V02,
				this.V10,
				this.V11,
				this.V12,
				this.V20,
				this.V21,
				this.V22
		};
	}

	/// <summary>
	/// Creates rotation matrix around Y axis.
	/// </summary>
	///
	/// <param name="radians">Rotation angle around Y axis in radians.</param>
	///
	/// <returns>Returns rotation matrix to rotate an object around Y axis.</returns>
	public static Matrix3x3 CreateRotationY(float radians)
	{
		Matrix3x3 result = default(Matrix3x3);
		float v = (float)Math.Cos((double)radians);
		float num = (float)Math.Sin((double)radians);
		result.V00 = (result.V22 = v);
		result.V02 = num;
		result.V20 = -num;
		result.V11 = 1f;
		return result;
	}

	/// <summary>
	/// Creates rotation matrix around X axis.
	/// </summary>
	///
	/// <param name="radians">Rotation angle around X axis in radians.</param>
	///
	/// <returns>Returns rotation matrix to rotate an object around X axis.</returns>
	public static Matrix3x3 CreateRotationX(float radians)
	{
		Matrix3x3 result = default(Matrix3x3);
		float v = (float)Math.Cos((double)radians);
		float num = (float)Math.Sin((double)radians);
		result.V11 = (result.V22 = v);
		result.V12 = -num;
		result.V21 = num;
		result.V00 = 1f;
		return result;
	}

	/// <summary>
	/// Creates rotation matrix around Z axis.
	/// </summary>
	///
	/// <param name="radians">Rotation angle around Z axis in radians.</param>
	///
	/// <returns>Returns rotation matrix to rotate an object around Z axis.</returns>
	public static Matrix3x3 CreateRotationZ(float radians)
	{
		Matrix3x3 result = default(Matrix3x3);
		float v = (float)Math.Cos((double)radians);
		float num = (float)Math.Sin((double)radians);
		result.V00 = (result.V11 = v);
		result.V01 = -num;
		result.V10 = num;
		result.V22 = 1f;
		return result;
	}

	/// <summary>
	/// Creates rotation matrix to rotate an object around X, Y and Z axes.
	/// </summary>
	///
	/// <param name="yaw">Rotation angle around Y axis in radians.</param>
	/// <param name="pitch">Rotation angle around X axis in radians.</param>
	/// <param name="roll">Rotation angle around Z axis in radians.</param>
	///
	/// <returns>Returns rotation matrix to rotate an object around all 3 axes.</returns>
	///
	/// <remarks>
	/// <para><note>The routine assumes roll-pitch-yaw rotation order, when creating rotation
	/// matrix, i.e. an object is first rotated around Z axis, then around X axis and finally around
	/// Y axis.</note></para>
	/// </remarks>
	public static Matrix3x3 CreateFromYawPitchRoll(float yaw, float pitch, float roll)
	{
		return Matrix3x3.CreateRotationY(yaw) * Matrix3x3.CreateRotationX(pitch) * Matrix3x3.CreateRotationZ(roll);
	}

	/// <summary>
	/// Extract rotation angles from the rotation matrix.
	/// </summary>
	///
	/// <param name="yaw">Extracted rotation angle around Y axis in radians.</param>
	/// <param name="pitch">Extracted rotation angle around X axis in radians.</param>
	/// <param name="roll">Extracted rotation angle around Z axis in radians.</param>
	///
	/// <remarks><para><note>The routine assumes roll-pitch-yaw rotation order when extracting rotation angle.
	/// Using extracted angles with the <see cref="M:AForge.Math.Matrix3x3.CreateFromYawPitchRoll(System.Single,System.Single,System.Single)" /> should provide same rotation matrix.
	/// </note></para>
	///
	/// <para><note>The method assumes the provided matrix represent valid rotation matrix.</note></para>
	///
	/// <para>Sample usage:</para>
	/// <code>
	/// // assume we have a rotation matrix created like this
	/// float yaw   = 10.0f / 180 * Math.PI;
	/// float pitch = 30.0f / 180 * Math.PI;
	/// float roll  = 45.0f / 180 * Math.PI;
	///
	/// Matrix3x3 rotationMatrix = Matrix3x3.CreateFromYawPitchRoll( yaw, pitch, roll );
	/// // ...
	///
	/// // now somewhere in the code you may want to get rotation
	/// // angles back from a matrix assuming same rotation order
	/// float extractedYaw;
	/// float extractedPitch;
	/// float extractedRoll;
	///
	/// rotation.ExtractYawPitchRoll( out extractedYaw, out extractedPitch, out extractedRoll );
	/// </code>
	/// </remarks>
	public void ExtractYawPitchRoll(out float yaw, out float pitch, out float roll)
	{
		yaw = (float)Math.Atan2((double)this.V02, (double)this.V22);
		pitch = (float)Math.Asin((double)(-(double)this.V12));
		roll = (float)Math.Atan2((double)this.V10, (double)this.V11);
	}

	/// <summary>
	/// Creates a matrix from 3 rows specified as vectors.
	/// </summary>
	///
	/// <param name="row0">First row of the matrix to create.</param>
	/// <param name="row1">Second row of the matrix to create.</param>
	/// <param name="row2">Third row of the matrix to create.</param>
	///
	/// <returns>Returns a matrix from specified rows.</returns>
	public static Matrix3x3 CreateFromRows(Vector3 row0, Vector3 row1, Vector3 row2)
	{
		return new Matrix3x3
		{
			V00 = row0.x,
			V01 = row0.y,
			V02 = row0.z,
			V10 = row1.x,
			V11 = row1.y,
			V12 = row1.z,
			V20 = row2.x,
			V21 = row2.y,
			V22 = row2.z
		};
	}

	/// <summary>
	/// Creates a matrix from 3 columns specified as vectors.
	/// </summary>
	///
	/// <param name="column0">First column of the matrix to create.</param>
	/// <param name="column1">Second column of the matrix to create.</param>
	/// <param name="column2">Third column of the matrix to create.</param>
	///
	/// <returns>Returns a matrix from specified columns.</returns>
	public static Matrix3x3 CreateFromColumns(Vector3 column0, Vector3 column1, Vector3 column2)
	{
		return new Matrix3x3
		{
			V00 = column0.x,
			V10 = column0.y,
			V20 = column0.z,
			V01 = column1.x,
			V11 = column1.y,
			V21 = column1.z,
			V02 = column2.x,
			V12 = column2.y,
			V22 = column2.z
		};
	}

	/// <summary>
	/// Creates a diagonal matrix using the specified vector as diagonal elements.
	/// </summary>
	///
	/// <param name="vector">Vector to use for diagonal elements of the matrix.</param>
	///
	/// <returns>Returns a diagonal matrix.</returns>
	public static Matrix3x3 CreateDiagonal(Vector3 vector)
	{
		return new Matrix3x3
		{
			V00 = vector.x,
			V11 = vector.y,
			V22 = vector.z
		};
	}

	/// <summary>
	/// Get row of the matrix.
	/// </summary>
	///
	/// <param name="index">Row index to get, [0, 2].</param>
	///
	/// <returns>Returns specified row of the matrix as a vector.</returns>
	///
	/// <exception cref="T:System.ArgumentException">Invalid row index was specified.</exception>
	public Vector3 GetRow(int index)
	{
		if (index < 0 || index > 2)
		{
			throw new ArgumentException("Invalid row index was specified.", "index");
		}
		if (index == 0)
		{
			return new Vector3(this.V00, this.V01, this.V02);
		}
		if (index != 1)
		{
			return new Vector3(this.V20, this.V21, this.V22);
		}
		return new Vector3(this.V10, this.V11, this.V12);
	}

	/// <summary>
	/// Get column of the matrix.
	/// </summary>
	///
	/// <param name="index">Column index to get, [0, 2].</param>
	///
	/// <returns>Returns specified column of the matrix as a vector.</returns>
	///
	/// <exception cref="T:System.ArgumentException">Invalid column index was specified.</exception>
	public Vector3 GetColumn(int index)
	{
		if (index < 0 || index > 2)
		{
			throw new ArgumentException("Invalid column index was specified.", "index");
		}
		if (index == 0)
		{
			return new Vector3(this.V00, this.V10, this.V20);
		}
		if (index != 1)
		{
			return new Vector3(this.V02, this.V12, this.V22);
		}
		return new Vector3(this.V01, this.V11, this.V21);
	}

	/// <summary>
	/// Multiplies two specified matrices.
	/// </summary>
	///
	/// <param name="matrix1">Matrix to multiply.</param>
	/// <param name="matrix2">Matrix to multiply by.</param>
	///
	/// <returns>Return new matrix, which the result of multiplication of the two specified matrices.</returns>
	public static Matrix3x3 operator *(Matrix3x3 matrix1, Matrix3x3 matrix2)
	{
		return new Matrix3x3
		{
			V00 = matrix1.V00 * matrix2.V00 + matrix1.V01 * matrix2.V10 + matrix1.V02 * matrix2.V20,
			V01 = matrix1.V00 * matrix2.V01 + matrix1.V01 * matrix2.V11 + matrix1.V02 * matrix2.V21,
			V02 = matrix1.V00 * matrix2.V02 + matrix1.V01 * matrix2.V12 + matrix1.V02 * matrix2.V22,
			V10 = matrix1.V10 * matrix2.V00 + matrix1.V11 * matrix2.V10 + matrix1.V12 * matrix2.V20,
			V11 = matrix1.V10 * matrix2.V01 + matrix1.V11 * matrix2.V11 + matrix1.V12 * matrix2.V21,
			V12 = matrix1.V10 * matrix2.V02 + matrix1.V11 * matrix2.V12 + matrix1.V12 * matrix2.V22,
			V20 = matrix1.V20 * matrix2.V00 + matrix1.V21 * matrix2.V10 + matrix1.V22 * matrix2.V20,
			V21 = matrix1.V20 * matrix2.V01 + matrix1.V21 * matrix2.V11 + matrix1.V22 * matrix2.V21,
			V22 = matrix1.V20 * matrix2.V02 + matrix1.V21 * matrix2.V12 + matrix1.V22 * matrix2.V22
		};
	}

	/// <summary>
	/// Multiplies two specified matrices.
	/// </summary>
	///
	/// <param name="matrix1">Matrix to multiply.</param>
	/// <param name="matrix2">Matrix to multiply by.</param>
	///
	/// <returns>Return new matrix, which the result of multiplication of the two specified matrices.</returns>
	public static Matrix3x3 Multiply(Matrix3x3 matrix1, Matrix3x3 matrix2)
	{
		return matrix1 * matrix2;
	}

	/// <summary>
	/// Adds corresponding components of two matrices.
	/// </summary>
	///
	/// <param name="matrix1">The matrix to add to.</param>
	/// <param name="matrix2">The matrix to add to the first matrix.</param>
	///
	/// <returns>Returns a matrix which components are equal to sum of corresponding
	/// components of the two specified matrices.</returns>
	public static Matrix3x3 operator +(Matrix3x3 matrix1, Matrix3x3 matrix2)
	{
		return new Matrix3x3
		{
			V00 = matrix1.V00 + matrix2.V00,
			V01 = matrix1.V01 + matrix2.V01,
			V02 = matrix1.V02 + matrix2.V02,
			V10 = matrix1.V10 + matrix2.V10,
			V11 = matrix1.V11 + matrix2.V11,
			V12 = matrix1.V12 + matrix2.V12,
			V20 = matrix1.V20 + matrix2.V20,
			V21 = matrix1.V21 + matrix2.V21,
			V22 = matrix1.V22 + matrix2.V22
		};
	}

	/// <summary>
	/// Adds corresponding components of two matrices.
	/// </summary>
	///
	/// <param name="matrix1">The matrix to add to.</param>
	/// <param name="matrix2">The matrix to add to the first matrix.</param>
	///
	/// <returns>Returns a matrix which components are equal to sum of corresponding
	/// components of the two specified matrices.</returns>
	public static Matrix3x3 Add(Matrix3x3 matrix1, Matrix3x3 matrix2)
	{
		return matrix1 + matrix2;
	}

	/// <summary>
	/// Subtracts corresponding components of two matrices.
	/// </summary>
	///
	/// <param name="matrix1">The matrix to subtract from.</param>
	/// <param name="matrix2">The matrix to subtract from the first matrix.</param>
	///
	/// <returns>Returns a matrix which components are equal to difference of corresponding
	/// components of the two specified matrices.</returns>
	public static Matrix3x3 operator -(Matrix3x3 matrix1, Matrix3x3 matrix2)
	{
		return new Matrix3x3
		{
			V00 = matrix1.V00 - matrix2.V00,
			V01 = matrix1.V01 - matrix2.V01,
			V02 = matrix1.V02 - matrix2.V02,
			V10 = matrix1.V10 - matrix2.V10,
			V11 = matrix1.V11 - matrix2.V11,
			V12 = matrix1.V12 - matrix2.V12,
			V20 = matrix1.V20 - matrix2.V20,
			V21 = matrix1.V21 - matrix2.V21,
			V22 = matrix1.V22 - matrix2.V22
		};
	}

	/// <summary>
	/// Subtracts corresponding components of two matrices.
	/// </summary>
	///
	/// <param name="matrix1">The matrix to subtract from.</param>
	/// <param name="matrix2">The matrix to subtract from the first matrix.</param>
	///
	/// <returns>Returns a matrix which components are equal to difference of corresponding
	/// components of the two specified matrices.</returns>
	public static Matrix3x3 Subtract(Matrix3x3 matrix1, Matrix3x3 matrix2)
	{
		return matrix1 - matrix2;
	}

	/// <summary>
	/// Multiplies specified matrix by the specified vector.
	/// </summary>
	///
	/// <param name="matrix">Matrix to multiply by vector.</param>
	/// <param name="vector">Vector to multiply matrix by.</param>
	///
	/// <returns>Returns new vector which is the result of multiplication of the specified matrix
	/// by the specified vector.</returns>
	public static Vector3 operator *(Matrix3x3 matrix, Vector3 vector)
	{
		return new Vector3(matrix.V00 * vector.x + matrix.V01 * vector.y + matrix.V02 * vector.z, matrix.V10 * vector.x + matrix.V11 * vector.y + matrix.V12 * vector.z, matrix.V20 * vector.x + matrix.V21 * vector.y + matrix.V22 * vector.z);
	}

	/// <summary>
	/// Multiplies specified matrix by the specified vector.
	/// </summary>
	///
	/// <param name="matrix">Matrix to multiply by vector.</param>
	/// <param name="vector">Vector to multiply matrix by.</param>
	///
	/// <returns>Returns new vector which is the result of multiplication of the specified matrix
	/// by the specified vector.</returns>
	public static Vector3 Multiply(Matrix3x3 matrix, Vector3 vector)
	{
		return matrix * vector;
	}

	/// <summary>
	/// Multiplies matrix by the specified factor.
	/// </summary>
	///
	/// <param name="matrix">Matrix to multiply.</param>
	/// <param name="factor">Factor to multiple the specified matrix by.</param>
	///
	/// <returns>Returns new matrix with all components equal to corresponding components of the
	/// specified matrix multiples by the specified factor.</returns>
	public static Matrix3x3 operator *(Matrix3x3 matrix, float factor)
	{
		return new Matrix3x3
		{
			V00 = matrix.V00 * factor,
			V01 = matrix.V01 * factor,
			V02 = matrix.V02 * factor,
			V10 = matrix.V10 * factor,
			V11 = matrix.V11 * factor,
			V12 = matrix.V12 * factor,
			V20 = matrix.V20 * factor,
			V21 = matrix.V21 * factor,
			V22 = matrix.V22 * factor
		};
	}

	/// <summary>
	/// Multiplies matrix by the specified factor.
	/// </summary>
	///
	/// <param name="matrix">Matrix to multiply.</param>
	/// <param name="factor">Factor to multiple the specified matrix by.</param>
	///
	/// <returns>Returns new matrix with all components equal to corresponding components of the
	/// specified matrix multiples by the specified factor.</returns>
	public static Matrix3x3 Multiply(Matrix3x3 matrix, float factor)
	{
		return matrix * factor;
	}

	/// <summary>
	/// Adds specified value to all components of the specified matrix.
	/// </summary>
	///
	/// <param name="matrix">Matrix to add value to.</param>
	/// <param name="value">Value to add to all components of the specified matrix.</param>
	///
	/// <returns>Returns new matrix with all components equal to corresponding components of the
	/// specified matrix increased by the specified value.</returns>
	public static Matrix3x3 operator +(Matrix3x3 matrix, float value)
	{
		return new Matrix3x3
		{
			V00 = matrix.V00 + value,
			V01 = matrix.V01 + value,
			V02 = matrix.V02 + value,
			V10 = matrix.V10 + value,
			V11 = matrix.V11 + value,
			V12 = matrix.V12 + value,
			V20 = matrix.V20 + value,
			V21 = matrix.V21 + value,
			V22 = matrix.V22 + value
		};
	}

	/// <summary>
	/// Adds specified value to all components of the specified matrix.
	/// </summary>
	///
	/// <param name="matrix">Matrix to add value to.</param>
	/// <param name="value">Value to add to all components of the specified matrix.</param>
	///
	/// <returns>Returns new matrix with all components equal to corresponding components of the
	/// specified matrix increased by the specified value.</returns>
	public static Matrix3x3 Add(Matrix3x3 matrix, float value)
	{
		return matrix + value;
	}

	/// <summary>
	/// Tests whether two specified matrices are equal.
	/// </summary>
	///
	/// <param name="matrix1">The left-hand matrix.</param>
	/// <param name="matrix2">The right-hand matrix.</param>
	///
	/// <returns>Returns <see langword="true" /> if the two matrices are equal or <see langword="false" /> otherwise.</returns>
	public static bool operator ==(Matrix3x3 matrix1, Matrix3x3 matrix2)
	{
		return matrix1.V00 == matrix2.V00 && matrix1.V01 == matrix2.V01 && matrix1.V02 == matrix2.V02 && matrix1.V10 == matrix2.V10 && matrix1.V11 == matrix2.V11 && matrix1.V12 == matrix2.V12 && matrix1.V20 == matrix2.V20 && matrix1.V21 == matrix2.V21 && matrix1.V22 == matrix2.V22;
	}

	/// <summary>
	/// Tests whether two specified matrices are not equal.
	/// </summary>
	///
	/// <param name="matrix1">The left-hand matrix.</param>
	/// <param name="matrix2">The right-hand matrix.</param>
	///
	/// <returns>Returns <see langword="true" /> if the two matrices are not equal or <see langword="false" /> otherwise.</returns>
	public static bool operator !=(Matrix3x3 matrix1, Matrix3x3 matrix2)
	{
		return matrix1.V00 != matrix2.V00 || matrix1.V01 != matrix2.V01 || matrix1.V02 != matrix2.V02 || matrix1.V10 != matrix2.V10 || matrix1.V11 != matrix2.V11 || matrix1.V12 != matrix2.V12 || matrix1.V20 != matrix2.V20 || matrix1.V21 != matrix2.V21 || matrix1.V22 != matrix2.V22;
	}

	/// <summary>
	/// Tests whether the matrix equals to the specified one.
	/// </summary>
	///
	/// <param name="matrix">The matrix to test equality with.</param>
	///
	/// <returns>Returns <see langword="true" /> if the two matrices are equal or <see langword="false" /> otherwise.</returns>
	public bool Equals(Matrix3x3 matrix)
	{
		return this == matrix;
	}

	/// <summary>
	/// Tests whether the matrix equals to the specified object.
	/// </summary>
	///
	/// <param name="obj">The object to test equality with.</param>
	///
	/// <returns>Returns <see langword="true" /> if the matrix equals to the specified object or <see langword="false" /> otherwise.</returns>
	public override bool Equals(object obj)
	{
		return obj is Matrix3x3 && this.Equals((Matrix3x3)obj);
	}

	/// <summary>
	/// Returns the hashcode for this instance.
	/// </summary>
	///
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return this.V00.GetHashCode() + this.V01.GetHashCode() + this.V02.GetHashCode() + this.V10.GetHashCode() + this.V11.GetHashCode() + this.V12.GetHashCode() + this.V20.GetHashCode() + this.V21.GetHashCode() + this.V22.GetHashCode();
	}

	/// <summary>
	/// Transpose the matrix, A<sup>T</sup>.
	/// </summary>
	///
	/// <returns>Return a matrix which equals to transposition of this matrix.</returns>
	public Matrix3x3 Transpose()
	{
		return new Matrix3x3
		{
			V00 = this.V00,
			V01 = this.V10,
			V02 = this.V20,
			V10 = this.V01,
			V11 = this.V11,
			V12 = this.V21,
			V20 = this.V02,
			V21 = this.V12,
			V22 = this.V22
		};
	}

	/// <summary>
	/// Multiply the matrix by its transposition, A*A<sup>T</sup>.
	/// </summary>
	///
	/// <returns>Returns a matrix which is the result of multiplying this matrix by its transposition.</returns>
	public Matrix3x3 MultiplySelfByTranspose()
	{
		Matrix3x3 result = default(Matrix3x3);
		result.V00 = this.V00 * this.V00 + this.V01 * this.V01 + this.V02 * this.V02;
		result.V10 = (result.V01 = this.V00 * this.V10 + this.V01 * this.V11 + this.V02 * this.V12);
		result.V20 = (result.V02 = this.V00 * this.V20 + this.V01 * this.V21 + this.V02 * this.V22);
		result.V11 = this.V10 * this.V10 + this.V11 * this.V11 + this.V12 * this.V12;
		result.V21 = (result.V12 = this.V10 * this.V20 + this.V11 * this.V21 + this.V12 * this.V22);
		result.V22 = this.V20 * this.V20 + this.V21 * this.V21 + this.V22 * this.V22;
		return result;
	}

	/// <summary>
	/// Multiply transposition of this matrix by itself, A<sup>T</sup>*A.
	/// </summary>
	///
	/// <returns>Returns a matrix which is the result of multiplying this matrix's transposition by itself.</returns>
	public Matrix3x3 MultiplyTransposeBySelf()
	{
		Matrix3x3 result = default(Matrix3x3);
		result.V00 = this.V00 * this.V00 + this.V10 * this.V10 + this.V20 * this.V20;
		result.V10 = (result.V01 = this.V00 * this.V01 + this.V10 * this.V11 + this.V20 * this.V21);
		result.V20 = (result.V02 = this.V00 * this.V02 + this.V10 * this.V12 + this.V20 * this.V22);
		result.V11 = this.V01 * this.V01 + this.V11 * this.V11 + this.V21 * this.V21;
		result.V21 = (result.V12 = this.V01 * this.V02 + this.V11 * this.V12 + this.V21 * this.V22);
		result.V22 = this.V02 * this.V02 + this.V12 * this.V12 + this.V22 * this.V22;
		return result;
	}

	/// <summary>
	/// Calculate adjugate of the matrix, adj(A).
	/// </summary>
	///
	/// <returns>Returns adjugate of the matrix.</returns>
	public Matrix3x3 Adjugate()
	{
		return new Matrix3x3
		{
			V00 = this.V11 * this.V22 - this.V12 * this.V21,
			V01 = -(this.V01 * this.V22 - this.V02 * this.V21),
			V02 = this.V01 * this.V12 - this.V02 * this.V11,
			V10 = -(this.V10 * this.V22 - this.V12 * this.V20),
			V11 = this.V00 * this.V22 - this.V02 * this.V20,
			V12 = -(this.V00 * this.V12 - this.V02 * this.V10),
			V20 = this.V10 * this.V21 - this.V11 * this.V20,
			V21 = -(this.V00 * this.V21 - this.V01 * this.V20),
			V22 = this.V00 * this.V11 - this.V01 * this.V10
		};
	}

	/// <summary>
	/// Calculate inverse of the matrix, A<sup>-1</sup>.
	/// </summary>
	///
	/// <returns>Returns inverse of the matrix.</returns>
	///
	/// <exception cref="T:System.ArgumentException">Cannot calculate inverse of the matrix since it is singular.</exception>
	public Matrix3x3 Inverse()
	{
		float determinant = this.Determinant;
		if (determinant == 0f)
		{
			throw new ArgumentException("Cannot calculate inverse of the matrix since it is singular.");
		}
		float num = 1f / determinant;
		Matrix3x3 result = this.Adjugate();
		result.V00 *= num;
		result.V01 *= num;
		result.V02 *= num;
		result.V10 *= num;
		result.V11 *= num;
		result.V12 *= num;
		result.V20 *= num;
		result.V21 *= num;
		result.V22 *= num;
		return result;
	}

	/// <summary>
	/// Calculate pseudo inverse of the matrix, A<sup>+</sup>.
	/// </summary>
	///
	/// <returns>Returns pseudo inverse of the matrix.</returns>
	///
	/// <remarks><para>The pseudo inverse of the matrix is calculate through its <see cref="M:AForge.Math.Matrix3x3.SVD(AForge.Math.Matrix3x3@,AForge.Math.Vector3@,AForge.Math.Matrix3x3@)" />
	/// as V*E<sup>+</sup>*U<sup>T</sup>.</para></remarks>
	public Matrix3x3 PseudoInverse()
	{
		Matrix3x3 matrix3x;
		Vector3 vector;
		Matrix3x3 matrix;
		this.SVD(out matrix3x, out vector, out matrix);
		return matrix * Matrix3x3.CreateDiagonal(vector.Inverse()) * matrix3x.Transpose();
	}

	/// <summary>
	/// Calculate Singular Value Decomposition (SVD) of the matrix, such as A=U*E*V<sup>T</sup>.
	/// </summary>
	///
	/// <param name="u">Output parameter which gets 3x3 U matrix.</param>
	/// <param name="e">Output parameter which gets diagonal elements of the E matrix.</param>
	/// <param name="v">Output parameter which gets 3x3 V matrix.</param>
	///
	/// <remarks><para>Having components U, E and V the source matrix can be reproduced using below code:
	/// <code>
	/// Matrix3x3 source = u * Matrix3x3.Diagonal( e ) * v.Transpose( );
	/// </code>
	/// </para></remarks>
	public void SVD(out Matrix3x3 u, out Vector3 e, out Matrix3x3 v)
	{
		double[,] array = new double[3, 3];
		array[0, 0] = (double)this.V00;
		array[0, 1] = (double)this.V01;
		array[0, 2] = (double)this.V02;
		array[1, 0] = (double)this.V10;
		array[1, 1] = (double)this.V11;
		array[1, 2] = (double)this.V12;
		array[2, 0] = (double)this.V20;
		array[2, 1] = (double)this.V21;
		array[2, 2] = (double)this.V22;
		double[,] array2 = array;
		double[] array3;
		double[,] array4;
		svd.svdcmp(array2, out array3, out array4);
		u = default(Matrix3x3);
		u.V00 = (float)array2[0, 0];
		u.V01 = (float)array2[0, 1];
		u.V02 = (float)array2[0, 2];
		u.V10 = (float)array2[1, 0];
		u.V11 = (float)array2[1, 1];
		u.V12 = (float)array2[1, 2];
		u.V20 = (float)array2[2, 0];
		u.V21 = (float)array2[2, 1];
		u.V22 = (float)array2[2, 2];
		v = default(Matrix3x3);
		v.V00 = (float)array4[0, 0];
		v.V01 = (float)array4[0, 1];
		v.V02 = (float)array4[0, 2];
		v.V10 = (float)array4[1, 0];
		v.V11 = (float)array4[1, 1];
		v.V12 = (float)array4[1, 2];
		v.V20 = (float)array4[2, 0];
		v.V21 = (float)array4[2, 1];
		v.V22 = (float)array4[2, 2];
		e = default(Vector3);
		e.x = (float)array3[0];
		e.y = (float)array3[1];
		e.z = (float)array3[2];
	}

	/// <summary>
	/// Row 0 column 0 element of the matrix.
	/// </summary>
	public float V00;

	/// <summary>
	/// Row 0 column 1 element of the matrix.
	/// </summary>
	public float V01;

	/// <summary>
	/// Row 0 column 2 element of the matrix.
	/// </summary>
	public float V02;

	/// <summary>
	/// Row 1 column 0 element of the matrix.
	/// </summary>
	public float V10;

	/// <summary>
	/// Row 1 column 1 element of the matrix.
	/// </summary>
	public float V11;

	/// <summary>
	/// Row 1 column 2 element of the matrix.
	/// </summary>
	public float V12;

	/// <summary>
	/// Row 2 column 0 element of the matrix.
	/// </summary>
	public float V20;

	/// <summary>
	/// Row 2 column 1 element of the matrix.
	/// </summary>
	public float V21;

	/// <summary>
	/// Row 2 column 2 element of the matrix.
	/// </summary>
	public float V22;
}

internal class svd
{
	public static void svdcmp(double[,] a, out double[] w, out double[,] v)
	{
		int length = a.GetLength(0);
		int length2 = a.GetLength(1);
		if (length < length2)
		{
			throw new ArgumentException("Number of rows in A must be greater or equal to number of columns");
		}
		w = new double[length2];
		v = new double[length2, length2];
		int i = 0;
		int num = 0;
		double[] array = new double[length2];
		double num4;
		double num3;
		double num2 = num3 = (num4 = 0.0);
		for (int j = 0; j < length2; j++)
		{
			i = j + 1;
			array[j] = num2 * num3;
			double num5 = num3 = (num2 = 0.0);
			if (j < length)
			{
				for (int k = j; k < length; k++)
				{
					num2 += Math.Abs(a[k, j]);
				}
				if (num2 != 0.0)
				{
					for (int k = j; k < length; k++)
					{
						a[k, j] /= num2;
						num5 += a[k, j] * a[k, j];
					}
					double num6 = a[j, j];
					num3 = -svd.Sign(Math.Sqrt(num5), num6);
					double num7 = num6 * num3 - num5;
					a[j, j] = num6 - num3;
					if (j != length2 - 1)
					{
						for (int l = i; l < length2; l++)
						{
							num5 = 0.0;
							for (int k = j; k < length; k++)
							{
								num5 += a[k, j] * a[k, l];
							}
							num6 = num5 / num7;
							for (int k = j; k < length; k++)
							{
								a[k, l] += num6 * a[k, j];
							}
						}
					}
					for (int k = j; k < length; k++)
					{
						a[k, j] *= num2;
					}
				}
			}
			w[j] = num2 * num3;
			num5 = (num3 = (num2 = 0.0));
			if (j < length && j != length2 - 1)
			{
				for (int k = i; k < length2; k++)
				{
					num2 += Math.Abs(a[j, k]);
				}
				if (num2 != 0.0)
				{
					for (int k = i; k < length2; k++)
					{
						a[j, k] /= num2;
						num5 += a[j, k] * a[j, k];
					}
					double num6 = a[j, i];
					num3 = -svd.Sign(Math.Sqrt(num5), num6);
					double num7 = num6 * num3 - num5;
					a[j, i] = num6 - num3;
					for (int k = i; k < length2; k++)
					{
						array[k] = a[j, k] / num7;
					}
					if (j != length - 1)
					{
						for (int l = i; l < length; l++)
						{
							num5 = 0.0;
							for (int k = i; k < length2; k++)
							{
								num5 += a[l, k] * a[j, k];
							}
							for (int k = i; k < length2; k++)
							{
								a[l, k] += num5 * array[k];
							}
						}
					}
					for (int k = i; k < length2; k++)
					{
						a[j, k] *= num2;
					}
				}
			}
			num4 = Math.Max(num4, Math.Abs(w[j]) + Math.Abs(array[j]));
		}
		for (int j = length2 - 1; j >= 0; j--)
		{
			if (j < length2 - 1)
			{
				if (num3 != 0.0)
				{
					for (int l = i; l < length2; l++)
					{
						v[l, j] = a[j, l] / a[j, i] / num3;
					}
					for (int l = i; l < length2; l++)
					{
						double num5 = 0.0;
						for (int k = i; k < length2; k++)
						{
							num5 += a[j, k] * v[k, l];
						}
						for (int k = i; k < length2; k++)
						{
							v[k, l] += num5 * v[k, j];
						}
					}
				}
				for (int l = i; l < length2; l++)
				{
					v[j, l] = (v[l, j] = 0.0);
				}
			}
			v[j, j] = 1.0;
			num3 = array[j];
			i = j;
		}
		for (int j = length2 - 1; j >= 0; j--)
		{
			i = j + 1;
			num3 = w[j];
			if (j < length2 - 1)
			{
				for (int l = i; l < length2; l++)
				{
					a[j, l] = 0.0;
				}
			}
			if (num3 != 0.0)
			{
				num3 = 1.0 / num3;
				if (j != length2 - 1)
				{
					for (int l = i; l < length2; l++)
					{
						double num5 = 0.0;
						for (int k = i; k < length; k++)
						{
							num5 += a[k, j] * a[k, l];
						}
						double num6 = num5 / a[j, j] * num3;
						for (int k = j; k < length; k++)
						{
							a[k, l] += num6 * a[k, j];
						}
					}
				}
				for (int l = j; l < length; l++)
				{
					a[l, j] *= num3;
				}
			}
			else
			{
				for (int l = j; l < length; l++)
				{
					a[l, j] = 0.0;
				}
			}
			a[j, j] += 1.0;
		}
		for (int k = length2 - 1; k >= 0; k--)
		{
			int m = 1;
			while (m <= 30)
			{
				int num8 = 1;
				for (i = k; i >= 0; i--)
				{
					num = i - 1;
					if (Math.Abs(array[i]) + num4 == num4)
					{
						num8 = 0;
						break;
					}
					if (Math.Abs(w[num]) + num4 == num4)
					{
						break;
					}
				}
				double num11;
				if (num8 != 0)
				{
					double num5 = 1.0;
					for (int j = i; j <= k; j++)
					{
						double num6 = num5 * array[j];
						if (Math.Abs(num6) + num4 != num4)
						{
							num3 = w[j];
							double num7 = svd.Pythag(num6, num3);
							w[j] = num7;
							num7 = 1.0 / num7;
							double num9 = num3 * num7;
							num5 = -num6 * num7;
							for (int l = 1; l <= length; l++)
							{
								double num10 = a[l, num];
								num11 = a[l, j];
								a[l, num] = num10 * num9 + num11 * num5;
								a[l, j] = num11 * num9 - num10 * num5;
							}
						}
					}
				}
				num11 = w[k];
				if (i == k)
				{
					if (num11 < 0.0)
					{
						w[k] = -num11;
						for (int l = 0; l < length2; l++)
						{
							v[l, k] = -v[l, k];
						}
						break;
					}
					break;
				}
				else
				{
					if (m == 30)
					{
						throw new ApplicationException("No convergence in 30 svdcmp iterations");
					}
					double num12 = w[i];
					num = k - 1;
					double num10 = w[num];
					num3 = array[num];
					double num7 = array[k];
					double num6 = ((num10 - num11) * (num10 + num11) + (num3 - num7) * (num3 + num7)) / (2.0 * num7 * num10);
					num3 = svd.Pythag(num6, 1.0);
					num6 = ((num12 - num11) * (num12 + num11) + num7 * (num10 / (num6 + svd.Sign(num3, num6)) - num7)) / num12;
					double num9;
					double num5 = num9 = 1.0;
					for (int l = i; l <= num; l++)
					{
						int j = l + 1;
						num3 = array[j];
						num10 = w[j];
						num7 = num5 * num3;
						num3 = num9 * num3;
						num11 = svd.Pythag(num6, num7);
						array[l] = num11;
						num9 = num6 / num11;
						num5 = num7 / num11;
						num6 = num12 * num9 + num3 * num5;
						num3 = num3 * num9 - num12 * num5;
						num7 = num10 * num5;
						num10 *= num9;
						for (int n = 0; n < length2; n++)
						{
							num12 = v[n, l];
							num11 = v[n, j];
							v[n, l] = num12 * num9 + num11 * num5;
							v[n, j] = num11 * num9 - num12 * num5;
						}
						num11 = svd.Pythag(num6, num7);
						w[l] = num11;
						if (num11 != 0.0)
						{
							num11 = 1.0 / num11;
							num9 = num6 * num11;
							num5 = num7 * num11;
						}
						num6 = num9 * num3 + num5 * num10;
						num12 = num9 * num10 - num5 * num3;
						for (int n = 0; n < length; n++)
						{
							num10 = a[n, l];
							num11 = a[n, j];
							a[n, l] = num10 * num9 + num11 * num5;
							a[n, j] = num11 * num9 - num10 * num5;
						}
					}
					array[i] = 0.0;
					array[k] = num6;
					w[k] = num12;
					m++;
				}
			}
		}
	}

	private static double Sign(double a, double b)
	{
		if (b < 0.0)
		{
			return -Math.Abs(a);
		}
		return Math.Abs(a);
	}

	private static double Pythag(double a, double b)
	{
		double num = Math.Abs(a);
		double num2 = Math.Abs(b);
		double result;
		if (num > num2)
		{
			double num3 = num2 / num;
			result = num * Math.Sqrt(1.0 + num3 * num3);
		}
		else if (num2 > 0.0)
		{
			double num3 = num / num2;
			result = num2 * Math.Sqrt(1.0 + num3 * num3);
		}
		else
		{
			result = 0.0;
		}
		return result;
	}
}


