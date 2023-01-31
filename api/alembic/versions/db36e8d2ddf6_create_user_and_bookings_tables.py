"""Create User and Bookings tables

Revision ID: db36e8d2ddf6
Revises: 
Create Date: 2023-01-31 15:38:31.124411

"""
import sqlalchemy as sa

from alembic import op

# revision identifiers, used by Alembic.
revision = "db36e8d2ddf6"
down_revision = None
branch_labels = None
depends_on = None


def upgrade() -> None:
    op.create_table(
        "users",
        sa.Column("id", sa.Integer(), nullable=False),
        sa.Column("name", sa.String(), nullable=False),
        sa.PrimaryKeyConstraint("id"),
    )

    op.create_table(
        "bookings",
        sa.Column("id", sa.Integer(), nullable=False),
        sa.Column("startDate", sa.DateTime(), nullable=False),
        sa.Column("endDate", sa.DateTime(), nullable=False),
        sa.Column("userId", sa.Integer(), nullable=False),
        sa.PrimaryKeyConstraint("id"),
    )


def downgrade() -> None:
    op.drop_table("users")
    op.drop_table("bookings")
